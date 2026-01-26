public enum ResultInfo
{
    Success = 200,

    //--- Success --- (starts with 1)
    SaveSuccess = 1000,
    Deleted = 1001,
    Activated = 1002,
    Deactivated = 1003,
    //-------------------------------

    //--- Fail --- (starts with 2)
    SaveFailure = 2000,
    BlockedOperation = 2001,
    NotFound = 2002,
    UserNamePasswordIncorrect = 2003,
    TokenExpired = 2004,
    TokenIsInvalid = 2005,
    UnexpectedError = 2006,
    InternalServerError = 2007,
    Unauthorized = 2008,
    FillRequiredFields = 2009,
    OrderServicesCannotBeEmpty = 2010,
    AlreadyApproved = 2011,
    CannotBeChanged = 2012,
    InvalidRequestParameters = 2013,
    OrderTransportDetailsCannotBeEmpty = 2014,
    ConfirmPasswordError = 2015,
    AlreadyExists = 2016,
    UserNameAlreadyExists = 2017,
    EmailAlreadyExists = 2018,
    InvoiceDetailsCannotBeEmpty = 2019,
    EmailSendingError = 2020,
    EmailNotFound = 2021,
    InvalidWagonNo = 2022,
    CurrentPasswordIncorrect = 2023,
    //-------------------------------

    NotImplemented = -1
}
