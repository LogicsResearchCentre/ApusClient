# ApusClient

- APUS ir atkritumu pārvadājumu uzskaites valsts informācijas sistēma http://parissrv.lvgmc.lv/APUS
- ApusClient ir .net Standard 2.0 bibliotēka darbam ar APUS sistēmu.

```c#
#define test //Aizkomentēt šo ridnu, lai darbotos produkcijas vidē!

using Apus;
using System;
using System.Linq;

#if test
var apusServiceUrl = "https://services.proofit.lv";
var apusUserName = "vanags.mikus@gmail.com";
var apusPassword = "password";
#else
var apusServiceUrl = "http://parissrv.lvgmc.lv";
var apusUserName = "vanags.mikus@gmail.com";
var apusPassword = "password";
#endif

using var apus = new ApusClient(apusServiceUrl, apusUserName, apusPassword);
var loggedIN = await apus.AuthenticateAsync();
if (loggedIN)
{
    var transportationDealNumber = "TransportationDealNumber123";
    var accountingSystemDealNumber = "Ferum123";
    var companyApusId = 1363942065791UL; //Tolmets Vidzeme uzņēmuma id APUS sistēmā
    var comapnyOfficeGarbageLicance = "VA12IB0030"; //atkritumu pārstrādes licenze Tolmets Vidzeme, Valmieras filiālei
    var comapnyOfficeGarbageLicanceIssueDate = new DateTime(2012, 6, 11);
    var companyOfficeFacilityCode = "BAQHCJQ"; //Adreses kods APUS sistēmā
    var companyEmployeeName = "Mikus Vanags";
    var companyEmployeePhone = "26673860";
    var sellerCompanyName = "Logics Research Centre SIA";
    var sellerCompanyRegistrationNumber = "40103512178";
    var sellerOrganizationType = "SIA"; //izmantot APUS testa vides dokumentāciju: https://services.proofit.lv/APUS/swagger-ui.html web-service-controller
    var sellerCompanyAddress = "Stērstu iela 7-6, Rīga, LV 1004";
    var sellerGarbageAddress = "Stērstu iela 7-6, Rīga, LV 1004"; //lūžņu atrašanās adrese
    var sellerEmployeeName = "FirstName LastName";
    var sellerEmployeePhone = "12345678";
    var transporterCompanyPhone = "12345678";
    var transporterCompanyGarbageTransportationLicence = "VA14AA0003"; //bīstamo atkritumu pārvadāšanā šis lauks APUS sistēmā ir obligāts
    var transporterVehicleNumber = "AB1234";
    var transporterTrailerNumber = "BC1234";
    var transporterDriverName = "Driver Name";

    var garbageItems = new InvoiceContentData[]
    {
        //Atkritumu kodi:
        //https://likumi.lv/ta/id/229148-noteikumi-par-atkritumu-klasifikatoru-un-ipasibam-kuras-padara-atkritumus-bistamus
        //vai izmantot APUS testa vides dokumentāciju: https://services.proofit.lv/APUS/swagger-ui.html web-service-controller
        
        new InvoiceContentData
        {
                AmountSend = 1.23M,
                AmountReceived = 1.23M,
                //klasifikācijas kodiem izmantot APUS testa vides dokumentāciju: https://services.proofit.lv/APUS/swagger-ui.html web-service-controller
                Target = new ClassifierDataPublic { Code = "RECYCLING_OR_REGISTRATION" },
                WasteClass = new ClassifierDataPublic { Code = "170402" }, //alumīnijs
                Number = 1 //row number
        },
        new InvoiceContentData
        {
                AmountSend = 2.34M,
                AmountReceived = 2.34M,
                Target = new ClassifierDataPublic { Code = "RECYCLING_OR_REGISTRATION" },
                WasteClass = new ClassifierDataPublic { Code = "170406" }, //alva
                Number = 2
        }
    };


    //pārbaudam vai darījuma partneris APUS sistēmā jau nav ievadījis pavadzīmi.
    //Pieņemam, ka partneris norāda transportēšanas darījuma numuru, pēc tā varam identificēt darījumu mūsu uzskaites sistēmā. Pretējā gadījumā APUS-ā būs darījuma dublikāti.
    var invoices = await apus.GetInvoicesByCarrierInvoiceNumberAsync(transportationDealNumber);
    InvoicePublic existingInvoice = null;
    if (invoices.Count > 0)
    {
        //šādiem ierakstiem nevajadzētu būt vairāk par 1, ja ir vairāk, tad kaut kas nav pareizi savadīts APUS sistēmā, visticamāk tie ir dublikāti. Ņemam pirmo!
        existingInvoice = await apus.GetInvoiceByApusInvoiceNumberAsync(invoices[0].InvoiceNumber);
    }

    var sellerCompany = await apus.GetOrganizationByRegistrationNumberAsync(sellerCompanyRegistrationNumber);
    if(sellerCompany == null)
    {
        //ja uzņēmums APUS-ā neeksistē, tad tas jāizveido
        var newOrgData = new OrganizationData
        {
            Name = sellerCompanyName,
            RegistrationNumber = sellerCompanyRegistrationNumber,
            Address = sellerCompanyAddress,
            OrganizationType = new ClassifierDataPublic { Code = sellerOrganizationType },
            IsForeign = false
        };
        sellerCompany = await apus.PostOrganizationAsync(newOrgData);
    }
    var sellerAddresses = await apus.GetFacilityAsync(FacilityGroupType.Senders, sellerCompany.Id);
    var sellerFacility = sellerAddresses.FirstOrDefault(n => n.FullAddress == sellerGarbageAddress);
    if(sellerFacility == null)
    {
        //ja uzņēmuma lūžņu pārdošanas adrese APUS-ā neeksistē, tad tā jāizveido
        var atvk = "0010000"; //Rīgas kods https://www.csb.gov.lv/sites/default/files/data/LV/atvk_2010_nov2011.xls
        var newFacility = ApusHelper.CreateFacility(sellerCompany.Id, sellerCompanyName, FacilityType.Sender, sellerGarbageAddress, atvk);
        sellerFacility = await apus.PostFacilityAsync(newFacility);
    }

    //ja pavadzīme eksistē APUS-ā, tad aizpildam tās datus ar informāciju no mūsu uzskaites sistēmas, pretējā gadījumā veidosies jauna APUS pavadzīme
    var apusInvoiceForSending = ApusHelper.CreateBuyingInvoiceFromJurPers(existingInvoice?.Invoice?.Number, accountingSystemDealNumber,
        existingInvoice?.Invoice?.ResponsibleOrganization?.Id ?? companyApusId, companyApusId, comapnyOfficeGarbageLicance,
        comapnyOfficeGarbageLicanceIssueDate, companyOfficeFacilityCode, sellerCompany.Id, sellerFacility.Code, sellerEmployeeName, sellerEmployeePhone,
        companyApusId, transporterCompanyGarbageTransportationLicence, transporterDriverName, transporterCompanyPhone, ShipmentType.SELF_TRANSPORT,
        TransportType.AUTO_TRANSPORT, transporterVehicleNumber, transporterTrailerNumber, DateTime.Now, garbageItems, companyEmployeeName, companyEmployeePhone);
    try
    {
        //nosūtam pavadzīmi atpakaļ uz APUS
        var savedApusInvoice = await apus.PostInvoiceAsync(apusInvoiceForSending);
        Console.WriteLine($"Izveidotās/atjauninātās Apus pavadzīmes numurs: {savedApusInvoice?.Invoice.Number}");
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.Message);
    }
}
else
{
    Console.WriteLine("Authentication failed!");
}

//Klase ApusHelper satur arī citus sia Tolmets Vidzeme darbā izmantotos šablonus
//(to izmantošana līdzīga kā iepriekš aplūkotajā ApusHelper.CreateBuyingInvoiceFromJurPers gadījumā):
//Iepirkšana no privātpersonām: ApusHelper.CreateBuyingInvoiceFromPrivatePers
//Iekšējā pārvietošana: ApusHelper.CreateInnerTransportationInvoice
//Starpniekdarījums: ApusHelper.CreateMediatorDealAllPartnersJurPers
//Pārdošana juridiskām personām: ApusHelper.CreateSellingInvoiceToJurPers

//Iespējams jūsu darba specifika būs nedaudz savādāka un darījumu datu šabloni jāveido nedaudz savādāk.
```

## Kontakti ar bibliotēkas izstrādātāju un uzturētāju
Mikus Vanags: mikus.vanags@logicsresearchcentre.com
