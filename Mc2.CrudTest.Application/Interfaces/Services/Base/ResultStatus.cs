namespace Mc2.CrudTest.Application.Interfaces.Services.Base
{
    /// <summary>
    /// وضعیت اجرای دستور را در لایه سرویس مشخص میکند.
    /// </summary>
    public enum ResultStatus
    {
        /// <summary>
        /// این مقدار نشان دهنده خطا در اعتبار سنجی می باشد
        /// </summary>
        ValidationError = -5,
        /// <summary>
        /// این مقدار نشان دهنده عدم احراز هویت می باشد
        /// </summary>
        Unauthorized = -4,
        /// <summary>
        /// این مقدار نشان دهنده وجود استثناء در اجرای دستور است
        /// </summary>
        Exception = -3,
        /// <summary>
        /// در مراحل اجرای دستور، دیتای مورد نظر یافت نشد
        /// </summary>
        DataNotFound = -2,
        /// <summary>
        /// اجرای دستور، ناموفق بود
        /// </summary>
        UnSuccessful = -1,
        /// <summary>
        /// وضعیت پیش فرض است
        /// </summary>
        UnKnown = 0,
        /// <summary>
        /// دستور با موفقیت اجرا شد
        /// </summary>
        Successful = 1

    }
}
