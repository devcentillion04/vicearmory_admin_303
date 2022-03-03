namespace Middleware.Infrastructure
{
    public class Constants
    {
        public const string API_CAPTION = "api";
        public const string API_VERSION = "v1";

        #region '---- API PATHS ----'

        public const string AUTHENTICATE = "Authenticate/Authenticate";
        //public const string IS_EMPLOYEE_AUTHENTICATED = "IsEmployeeAuthenticated";

        #endregion

        #region '---- Product APIs'
        public const string GETPRODUCT = "Product/GetProduct";
        public const string GETPRODUCTBYCATEGORYID = "Product/GetProductByCategoryId";
        public const string CREATEPRODUCT = "Product/CreateProduct";
        public const string UPDATEPRODUCT = "Product/UpdateProduct";
        public const string DELETEPRODUCTBYID = "Product/DeleteProductById";
        public const string GETPRODUCTATTRIBUTESBYPRODUCTID = "Product/GetProductAttributesByProductId";
        public const string GETPRODUCTREVIEWSBYPRODUCTID = "Product/GetProductReviewsByProductId";
        public const string GETPRODUCTIMAGESBYPRODUCTID = "Product/GetProductImagesByProductId";
        public const string ADDPRODUCTATTRIBUTES = "Product/AddProductAttributes";
        public const string ADDPRODUCTREVIEW = "Product/AddProductReview";
        public const string ADDPRODUCTIMAGES = "Product/AddProductImages";
        public const string GETPRODUCTS = "Product/GetProducts";
        public const string ADDLOGS = "Logs/AddLogs";
        #endregion

        #region '---- Menu -----'

        public const string GETMENU = "Menu/GetMenu";
        public const string GETMENUBYID = "Menu/GetMenuById";
        public const string DELETEMENUBYID = "Menu/DeleteMenuById";
        public const string CREATEMENU = "Menu/CreateMenu";
        public const string UPDATEMENU = "Menu/UpdateMenu";


        #endregion

        #region '---- Newsletter -----'

        public const string GETNEWSLETTERS = "Newsletter/GetNewsletters";
        public const string GETNEWSLETTERBYID = "Newsletter/GetNewsletterById";
        public const string DELETENEWSLETTERBYID = "Newsletter/DeleteNewsletterById";
        public const string CREATENEWSLETTERS = "Newsletter/CreateNewsletters";
        public const string UPDATEDNEWSLETTERS = "Newsletter/UpdatedNewsletters";


        #endregion

        #region '----WeeklyAds APIs----'
        public const string GETPDFBYID= "WeeklyAds/GetPdfById";
        public const string GETALLPDF= "WeeklyAds/GetAllPdf";
        public const string DELETEPDF= "WeeklyAds/DeletePdf";
        public const string ACTIVATEPDF = "WeeklyAds/ActivatePdf";
        public const string UPDATEPDF= "WeeklyAds/UpadtePdf";
        public const string ADDPDF= "WeeklyAds/AddPdf";
        #endregion

        #region '----User APIs----'
        public const string CREATEUSER = "User/CreateUser";
        public const string VERIFYUSER = "User/VerifyUser";
        public const string CREATEUSERERROR = "Error while create user";
        public const string CREATEUSERSUCCESS = "user created";
        public const string CREATEUSEREXIST = "Email is already exist";
        public const string CREATEUSERMAILSENT = "User is created please verify your mail";
        public const string EMAILVERIFIED = "EMAILVERIFIED";
        public const string EMAILEXPIRED = "EMAILEXPIRED";
        public const string EMAILNOTVERIFIED = "EMAILNOTVERIFIED";
        #endregion

    }
}
