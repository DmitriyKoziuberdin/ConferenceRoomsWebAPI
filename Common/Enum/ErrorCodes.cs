namespace Common.Enum
{
    public enum ErrorCodes
    {
        Undefined = 0,

        ConferenceRoom = 1_000,
        ConferenceRoomNotFound = 1_001,
        ConferenceRoomDuplicateName = 1_002,

        CompanyService = 2_000,
        CompanyServiceNotFound = 2_001,
        CompanyServiceDuplicateName = 2_002,

        Booking = 3_000,
        BookingNotFound = 3_001,
        BookingIsNotAvailable = 3_002,

        //BankCard = 4_000,
        //BankCardNotFound = 4_001,
        //BankCardDuplicateNumber = 4_002
    }
}
