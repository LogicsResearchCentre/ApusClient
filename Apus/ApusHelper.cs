using System;

namespace Apus
{
    public static class ApusHelper
    {
        public static InvoicePublic CreateBuyingInvoiceFromPrivatePers(string existingApusInvoiceNumber, ulong buyerOrganizationId, string managementLicence, string buyerFacilityCode, DateTime dealDate,
            InvoiceContentData[] invoiceItems, string employeeName, string employeePhone)
        {
            return new InvoicePublic()
            {
                Invoice = new InvoiceData
                {
                    ResponsibleOrganization = new OrganizationData { Id = buyerOrganizationId },
                    Number = existingApusInvoiceNumber
                },
                Sender = new SenderData
                {
                    LegalType = "LAT",
                    ShipmentDate = dealDate.ToString("yyyy-MM-ddTHH:mm:ss.fffzzz")
                },
                Receiver = new ReceiverData
                {
                    LegalType = "JUR",
                    Organization = new OrganizationData { Id = buyerOrganizationId },
                    ManagementLicense = new LicenseData { RegNumber = managementLicence },
                    Facility = new FacilityPublic { Code = buyerFacilityCode },
                    PersonResponsibleForWasteReceivingName = employeeName,
                    PersonResponsibleForWasteReceivingPhone = employeePhone,
                    ReceivedDate = dealDate.ToString("yyyy-MM-ddTHH:mm:ss.fffzzz"),
                },
                Content = invoiceItems
            };
        }

        public static InvoicePublic CreateBuyingInvoiceFromJurPers(string existingApusInvoiceNumber, string invoiceNumber, ulong responsibleOrganizationId, ulong buyerOrganizationId, string buyerManagementLicence,
            DateTime? buyerManagementLicenceIssueDate, string buyerFacilityCode,
            ulong sellerOrganizationId, string SellerFacilityCode,
            string sellerPersonName, string sellerPersonPhone, ulong transporterOrganizationId, string transporterLicence, string transporterPersonName, string transporterPersonPhone,
            ShipmentType shipmentType, TransportType transportType, string vehicleNumber, string trailerNumber,
            DateTime dealDate, InvoiceContentData[] invoiceItems, string employeeName, string employeePhone)
        {
            return new InvoicePublic()
            {
                Invoice = new InvoiceData
                {
                    ResponsibleOrganization = new OrganizationData { Id = responsibleOrganizationId },
                    Number = existingApusInvoiceNumber
                },
                Sender = new SenderData
                {
                    LegalType = "JUR",
                    Organization = new OrganizationData { Id = sellerOrganizationId },
                    Facility = new FacilityPublic { Code = SellerFacilityCode },
                    ShipmentDate = dealDate.ToString("yyyy-MM-ddTHH:mm:ss.fffzzz"),
                    PersonResponsibleForWasteShipmentName = sellerPersonName,
                    PersonResponsibleForWasteShipmentPhone = sellerPersonPhone
                },
                Receiver = new ReceiverData
                {
                    LegalType = "JUR",
                    Organization = new OrganizationData { Id = buyerOrganizationId },
                    ManagementLicense = new LicenseData { RegNumber = buyerManagementLicence },
                    Facility = new FacilityPublic { Code = buyerFacilityCode },
                    PersonResponsibleForWasteReceivingName = employeeName,
                    PersonResponsibleForWasteReceivingPhone = employeePhone,
                    ReceivedDate = dealDate.ToString("yyyy-MM-ddTHH:mm:ss.fffzzz")
                },
                Carriers = new CarrierData[] {
                    new CarrierData
                    {
                        Organization = new OrganizationData { Id = transporterOrganizationId },
                        License = new LicenseData{ RegNumber = transporterLicence },
                        PersonResponsibleForWasteTransportingName = transporterPersonName,
                        PersonResponsibleForWasteTransportingPhone = transporterPersonPhone,
                        TypeOfShipment = new ClassifierDataPublic{ Code = shipmentType.ToString() },
                        TypeOfTransport = new ClassifierDataPublic { Code = transportType.ToString() },
                        VehicleRegistrationNumber = vehicleNumber,
                        TrailerRegistrationNumber = trailerNumber,
                        CargoInvoiceNumber = invoiceNumber,
                    }
                },
                Content = invoiceItems
            };
        }

        public static InvoicePublic CreateSellingInvoiceToJurPers(string existingApusInvoiceNumber, string invoiceNumber, ulong responsibleOrganizationId, ulong buyerOrganizationId, string buyerManagementLicence, string buyerFacilityCode,
            ulong sellerOrganizationId, string SellerFacilityCode,
            string buyerPersonName, string buyerPersonPhone, ulong transporterOrganizationId, string transporterLicence, string transporterPersonName, string transporterPersonPhone,
            ShipmentType shipmentType, TransportType transportType, string vehicleNumber, string trailerNumber,
            DateTime dealDate, InvoiceContentData[] invoiceItems, string sellerPersonName, string sellerPersonPhone)
        {
            return new InvoicePublic()
            {
                Invoice = new InvoiceData
                {
                    ResponsibleOrganization = new OrganizationData { Id = responsibleOrganizationId },
                    Number = existingApusInvoiceNumber
                },
                Sender = new SenderData
                {
                    LegalType = "JUR",
                    Organization = new OrganizationData { Id = sellerOrganizationId },
                    Facility = new FacilityPublic { Code = SellerFacilityCode },
                    ShipmentDate = dealDate.ToString("yyyy-MM-ddTHH:mm:ss.fffzzz"),
                    PersonResponsibleForWasteShipmentName = sellerPersonName,
                    PersonResponsibleForWasteShipmentPhone = sellerPersonPhone
                },
                Receiver = new ReceiverData
                {
                    LegalType = "JUR",
                    Organization = new OrganizationData { Id = buyerOrganizationId },
                    ManagementLicense = new LicenseData { RegNumber = buyerManagementLicence },
                    Facility = new FacilityPublic { Code = buyerFacilityCode },
                    PersonResponsibleForWasteReceivingName = buyerPersonName,
                    PersonResponsibleForWasteReceivingPhone = buyerPersonPhone,
                    ReceivedDate = dealDate.ToString("yyyy-MM-ddTHH:mm:ss.fffzzz")
                },
                Carriers = new CarrierData[] {
                    new CarrierData
                    {
                        Organization = new OrganizationData { Id = transporterOrganizationId },
                        License = new LicenseData{ RegNumber = transporterLicence },
                        PersonResponsibleForWasteTransportingName = transporterPersonName,
                        PersonResponsibleForWasteTransportingPhone = transporterPersonPhone,
                        TypeOfShipment = new ClassifierDataPublic{ Code = shipmentType.ToString() },
                        TypeOfTransport = new ClassifierDataPublic { Code = transportType.ToString() },
                        VehicleRegistrationNumber = vehicleNumber,
                        TrailerRegistrationNumber = trailerNumber,
                        CargoInvoiceNumber = invoiceNumber,
                    }
                },
                Content = invoiceItems
            };
        }

        public static InvoicePublic CreateMediatorDealAllPartnersJurPers(string existingApusInvoiceNumber, string buyingInvoiceNumber, string sellingInvoiceNumber, ulong mediatorOrganizationId, string mediatorLicenceNumber, DateTime mediatorLicenceDate,
            string mediatorPersonName, string mediatorPhone, ulong buyerOrganizationId, string buyerManagementLicence, string buyerFacilityCode, string buyerPersonName, string buyerPersonPhone,
            ulong sellerOrganizationId, string sellerFacilityCode, string sellerPersonName, string sellerPersonPhone, ulong transporterOrganizationId, string transporterLicence, string transporterPersonName,
            string transporterPersonPhone, ShipmentType shipmentType, TransportType transportType, string vehicleNumber, string trailerNumber, DateTime dealDate, InvoiceContentData[] invoiceItems)
        {
            return new InvoicePublic()
            {
                Invoice = new InvoiceData
                {
                    ResponsibleOrganization = new OrganizationData { Id = mediatorOrganizationId },
                    Number = existingApusInvoiceNumber
                },
                Mediator = new MediatorData
                {
                    Organization = new OrganizationData { Id = mediatorOrganizationId },
                    vvdDecisionNumber = mediatorLicenceNumber,
                    VvdDecisionDate = mediatorLicenceDate.ToString("yyyy-MM-ddTHH:mm:ss.fffzzz"),
                    
                    PersonResponsibleForWasteMediationName = mediatorPersonName,
                    PersonResponsibleForWasteMediationPhone = mediatorPhone
                },
                Sender = new SenderData
                {
                    LegalType = "JUR",
                    Organization = new OrganizationData { Id = sellerOrganizationId },
                    Facility = new FacilityPublic { Code = sellerFacilityCode },
                    ShipmentDate = dealDate.ToString("yyyy-MM-ddTHH:mm:ss.fffzzz"),
                    PersonResponsibleForWasteShipmentName = sellerPersonName,
                    PersonResponsibleForWasteShipmentPhone = sellerPersonPhone
                },
                Receiver = new ReceiverData
                {
                    LegalType = "JUR",
                    Organization = new OrganizationData { Id = buyerOrganizationId },
                    ManagementLicense = new LicenseData { RegNumber = buyerManagementLicence },
                    Facility = new FacilityPublic { Code = buyerFacilityCode },
                    PersonResponsibleForWasteReceivingName = buyerPersonName,
                    PersonResponsibleForWasteReceivingPhone = buyerPersonPhone,
                    ReceivedDate = dealDate.ToString("yyyy-MM-ddTHH:mm:ss.fffzzz")
                },
                Carriers = new CarrierData[] {
                     new CarrierData
                    {
                        Organization = new OrganizationData { Id = transporterOrganizationId },
                        License = new LicenseData{ RegNumber = transporterLicence },
                        PersonResponsibleForWasteTransportingName = transporterPersonName,
                        PersonResponsibleForWasteTransportingPhone = transporterPersonPhone,
                        TypeOfShipment = new ClassifierDataPublic{ Code = shipmentType.ToString() },
                        TypeOfTransport = new ClassifierDataPublic { Code = transportType.ToString() },
                        VehicleRegistrationNumber = vehicleNumber,
                        TrailerRegistrationNumber = trailerNumber,
                        CargoInvoiceNumber = buyingInvoiceNumber,
                    },
                    new CarrierData
                    {
                        Organization = new OrganizationData { Id = transporterOrganizationId },
                        License = new LicenseData{ RegNumber = transporterLicence },
                        PersonResponsibleForWasteTransportingName = transporterPersonName,
                        PersonResponsibleForWasteTransportingPhone = transporterPersonPhone,
                        TypeOfShipment = new ClassifierDataPublic{ Code = shipmentType.ToString() },
                        TypeOfTransport = new ClassifierDataPublic { Code = transportType.ToString() },
                        VehicleRegistrationNumber = vehicleNumber,
                        TrailerRegistrationNumber = trailerNumber,
                        CargoInvoiceNumber = sellingInvoiceNumber,
                    }
                },
                Content = invoiceItems
            };
        }

        public static InvoicePublic CreateInnerTransportationInvoice(string existingApusInvoiceNumber , string invoiceNumber, ulong organizationId, string senderFacilityCode, string senderPersonName, string senderPersonPhone,
            string receiverManagementLicence, string receiverFacilityCode, string receiverPersonName, string receiverPersonPhone, ulong transporterOrganizationId, string transporterLicence,
            string transporterPersonName, string transporterPersonPhone, ShipmentType shipmentType, TransportType transportType, string vehicleNumber, string trailerNumber,
            DateTime dealDate, InvoiceContentData[] invoiceItems)
        {
            return new InvoicePublic()
            {
                Invoice = new InvoiceData
                {
                    ResponsibleOrganization = new OrganizationData { Id = organizationId },
                    Number = existingApusInvoiceNumber
                },
                Sender = new SenderData
                {
                    LegalType = "JUR",
                    Organization = new OrganizationData { Id = organizationId },
                    Facility = new FacilityPublic { Code = senderFacilityCode },
                    ShipmentDate = dealDate.ToString("yyyy-MM-ddTHH:mm:ss.fffzzz"),
                    PersonResponsibleForWasteShipmentName = senderPersonName,
                    PersonResponsibleForWasteShipmentPhone = senderPersonPhone
                },
                Receiver = new ReceiverData
                {
                    LegalType = "JUR",
                    Organization = new OrganizationData { Id = organizationId },
                    ManagementLicense = new LicenseData { RegNumber = receiverManagementLicence },
                    Facility = new FacilityPublic { Code = receiverFacilityCode },
                    ReceivedDate = dealDate.ToString("yyyy-MM-ddTHH:mm:ss.fffzzz"),
                    PersonResponsibleForWasteReceivingName = receiverPersonName,
                    PersonResponsibleForWasteReceivingPhone = receiverPersonPhone
                },
                Carriers = new CarrierData[] {
                    new CarrierData
                    {
                        Organization = new OrganizationData { Id = transporterOrganizationId },
                        License = new LicenseData{ RegNumber = transporterLicence },
                        PersonResponsibleForWasteTransportingName = transporterPersonName,
                        PersonResponsibleForWasteTransportingPhone = transporterPersonPhone,
                        TypeOfShipment = new ClassifierDataPublic{ Code = shipmentType.ToString() },
                        TypeOfTransport = new ClassifierDataPublic { Code = transportType.ToString() },
                        VehicleRegistrationNumber = vehicleNumber,
                        TrailerRegistrationNumber = trailerNumber,
                        CargoInvoiceNumber = invoiceNumber,
                    }
                },
                Content = invoiceItems
            };
        }

        public static FacilityPublic CreateFacility(ulong organizationId, string name, FacilityType facilityType, string fullAddress, string atvkCode)
        {
            return new FacilityPublic()
            {
                Name = name,
                Organization = new OrganizationData { Id = organizationId },
                FullAddress = fullAddress,
                TypeCode = GetClassifierData(facilityType),
                AtvkCode = atvkCode

            };
        }

        public static FacilityPublic CreateForeignFacility(ulong organizationId, string name, FacilityType facilityType, string fullAddress)
        {
            return new FacilityPublic()
            {
                Name = name,
                Organization = new OrganizationData { Id = organizationId },
                FullAddress = fullAddress,
                TypeCode = GetClassifierData(facilityType)
            };
        }

        private static ClassifierDataPublic GetClassifierData(FacilityType facilityType)
        {
            switch (facilityType)
            {
                case FacilityType.Sender:
                    return new ClassifierDataPublic { Code = "BANOS" };
                case FacilityType.Receiver:
                    return new ClassifierDataPublic { Code = "BASAN" };
                case FacilityType.SenderAndReceiver:
                    return new ClassifierDataPublic { Code = "BANS" };
                default:
                    return null;
            }
        }
    }
}
