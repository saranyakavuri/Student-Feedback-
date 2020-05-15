using Gios_mvcSolution.Models;
using Gios_mvcSolution.Areas.UseCase.Models;
using Gios_mvcSolution.Areas.UseCase.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gios_mvcSolution.DAL
{
    public class UseCase_DAL
    {
        SVC svc_DAL = new SVC();

        #region Required Support Types

        public string CreateEditRequiredSupportType(string AddUpdateFlag, RequiredSupportTypes requiredSupportTypes)
        {
            string _retVal;
            SqlConnection objCon = new SqlConnection();
            SqlCommand objCom;
            try
            {
                ConnectionString(objCon);

                objCom = new SqlCommand("GIOS.GIOS_UCAddUpdateRequiredSupportType", objCon);
                objCom.CommandType = System.Data.CommandType.StoredProcedure;
                objCom.Parameters.Add(new SqlParameter("@AddupdateFlag", AddUpdateFlag));
                objCom.Parameters.Add(new SqlParameter("@strSupportType", requiredSupportTypes.strSupportType));
                objCom.Parameters.Add(new SqlParameter("@strSupportTypeDescr", requiredSupportTypes.strSupportTypeDescr));

                if (AddUpdateFlag == "I")
                {
                    objCom.Parameters.Add(new SqlParameter("@strUserID", requiredSupportTypes.strCreatorID));
                    objCom.Parameters.Add(new SqlParameter("@intSupportTypeID", requiredSupportTypes.intSupportTypeID));
                }//Need to pass blank value since its new
                else
                {
                    objCom.Parameters.Add(new SqlParameter("@strUserID", requiredSupportTypes.strModifierID));
                    objCom.Parameters.Add(new SqlParameter("@intSupportTypeID", requiredSupportTypes.intSupportTypeID));
                }
                objCon.Open();
                objCom.ExecuteScalar();
                objCon.Close();
                _retVal = "Sucess";

            }
            catch (Exception ex)
            {
                _retVal = ex.Message;
            }

            return _retVal;

        }

        public RequiredSupportTypes FindRequiredSupportType(int id)
        {
            RequiredSupportTypes _retVal = new RequiredSupportTypes();
            SqlConnection objCon = new SqlConnection();
            SqlCommand objCom;

            ConnectionString(objCon);
            objCom = new SqlCommand("Select intSupportTypeID,strSupportType,strSupportTypeDescr,dtmCreated,strCreatorID,dtmModified,strModifierID FROM GIOS.GIOS_URequiredSupportTypes where intSupportTypeID = " + id , objCon);

            objCon.Open();
            SqlDataReader reader = objCom.ExecuteReader();
            while (reader.Read())
            {
                _retVal.intSupportTypeID = Convert.ToInt32(reader["intSupportTypeID"]);
                _retVal.strSupportType = reader["strSupportType"].ToString();
                _retVal.strSupportTypeDescr = reader["strSupportTypeDescr"].ToString();
                _retVal.dtmCreated = Convert.ToDateTime(reader["dtmCreated"]);
                _retVal.strCreatorID = reader["strCreatorID"].ToString();
                if (reader["dtmModified"] == System.DBNull.Value) { _retVal.dtmModified = null; }
                else { _retVal.dtmModified = Convert.ToDateTime(reader["dtmModified"]); }
                _retVal.strModifierID = reader["strModifierID"].ToString();
            }
            reader.Close();
            objCon.Close();
            return _retVal;

        }

        public IEnumerable<RequiredSupportTypes> ListrequiredSupportTypes()
        {
            List<RequiredSupportTypes> _retVal = new List<RequiredSupportTypes>();
            SqlConnection objCon = new SqlConnection();
            SqlCommand objCom;
            RequiredSupportTypes reqSup;

            ConnectionString(objCon);
            objCom = new SqlCommand("Select intSupportTypeID,strSupportType,strSupportTypeDescr,dtmCreated,strCreatorID,dtmModified,strModifierID FROM GIOS.GIOS_URequiredSupportTypes", objCon);

            objCon.Open();
            SqlDataReader reader = objCom.ExecuteReader();
            while (reader.Read())
            {
                reqSup = new RequiredSupportTypes();
                reqSup.intSupportTypeID = Convert.ToInt32(reader["intSupportTypeID"]);
                reqSup.strSupportType = reader["strSupportType"].ToString();
                reqSup.strSupportTypeDescr = reader["strSupportTypeDescr"].ToString();
                reqSup.dtmCreated = Convert.ToDateTime(reader["dtmCreated"]);
                reqSup.strCreatorID = reader["strCreatorID"].ToString();
                if (reader["dtmModified"] == System.DBNull.Value) { reqSup.dtmModified = null; }
                else { reqSup.dtmModified = Convert.ToDateTime(reader["dtmModified"]); }
                reqSup.strModifierID = reader["strModifierID"].ToString();

                _retVal.Add(reqSup);

            }
            reader.Close();
            objCon.Close();
            return _retVal;
        }
        
        public IEnumerable<string> GetUseCaseExtraSupport(int intUseCaseID)
        {
            List<string> _retVal = new List<string>();
            SqlConnection objCon = new SqlConnection();
            SqlCommand objCom;
            // UseCaseImpactKPI useCaseImpactKPI;
            try
            {
                ConnectionString(objCon);
                objCom = new SqlCommand("SELECT strSupportType from GIOS.GIOS_UUseCaseExtraSupport WHERE intUseCaseID  = @intUseCaseID", objCon);
                objCom.Parameters.Add(new SqlParameter("@intUseCaseID", intUseCaseID));

                objCon.Open();
                SqlDataReader reader = objCom.ExecuteReader();
                while (reader.Read())
                {
                    //  useCaseImpactKPI = new UseCaseImpactKPI();
                    //  useCaseImpactKPI.strKPI = reader["strKPI"].ToString();
                    string val = reader["strSupportTYpe"].ToString();
                    _retVal.Add(val);
                }

                reader.Close();
                objCon.Close();
                return _retVal;
            }
            catch (Exception ex)
            {
                return _retVal;
            }
        }

        public void RemoveUseCaseExtraSupport(int intUseCaseID)
        {
            SqlConnection objCon = new SqlConnection();
            SqlCommand objCom;
            try
            {
                ConnectionString(objCon);
                objCom = new SqlCommand("DELETE from GIOS.GIOS_UUseCaseExtraSupport where intUseCaseID = @intUseCaseID", objCon);
                objCom.Parameters.Add(new SqlParameter("@intUseCaseID", intUseCaseID));
                objCon.Open();

                objCom.ExecuteScalar();
                objCon.Close();
            }
            catch (Exception ex)
            {

            }
        }

        #endregion

        #region Impact KPI Functions

        public IEnumerable<ImpactKPI> ListImpactKPIs()
        {
            List<ImpactKPI> _retVal = new List<ImpactKPI>();
            SqlConnection objCon = new SqlConnection();
            SqlCommand objCom;
            ImpactKPI impactKPI;

            ConnectionString(objCon);
            objCom = new SqlCommand("SELECT intImpactKPIID, strKPI, strKPIDescr, dtmCreated, strCreatorID, dtmModified, strModifierID from GIOS.GIOS_UImpactKPI", objCon);

            objCon.Open();
            SqlDataReader reader = objCom.ExecuteReader();
            while (reader.Read())
            {
                impactKPI = new ImpactKPI();
                impactKPI.intImpactKPIID = Convert.ToInt32(reader["intImpactKPIID"]);
                impactKPI.strKPI = reader["strKPI"].ToString();
                impactKPI.strKPIDescr = reader["strKPIDescr"].ToString();
                impactKPI.dtmCreated = Convert.ToDateTime(reader["dtmCreated"]);
                impactKPI.strCreatorID = reader["strCreatorID"].ToString();
                if (reader["dtmModified"] == System.DBNull.Value) { impactKPI.dtmModified = null; }
                else { impactKPI.dtmModified = Convert.ToDateTime(reader["dtmModified"]); }
                impactKPI.strModifierID = reader["strModifierID"].ToString();

                _retVal.Add(impactKPI);

            }
            reader.Close();
            objCon.Close();
            return _retVal;
        }

        public string CreateEditImpactKPI(string AddUpdateFlag, ImpactKPI impactKPI)
        {
            string _retVal;
            SqlConnection objCon = new SqlConnection();
            SqlCommand objCom;
            try
            {
                ConnectionString(objCon);

                objCom = new SqlCommand("GIOS.GIOS_UCAddUpdateImpactKPI", objCon);
                objCom.CommandType = System.Data.CommandType.StoredProcedure;
                objCom.Parameters.Add(new SqlParameter("@AddupdateFlag", AddUpdateFlag));
                objCom.Parameters.Add(new SqlParameter("@strKPI", impactKPI.strKPI));
                objCom.Parameters.Add(new SqlParameter("@strKPIDescr", impactKPI.strKPIDescr));

                if (AddUpdateFlag == "I")
                {
                    objCom.Parameters.Add(new SqlParameter("@strUserID", impactKPI.strCreatorID));
                }//Need to pass blank value since its new
                else
                {
                    objCom.Parameters.Add(new SqlParameter("@strUserID", impactKPI.strModifierID));
                }
                objCon.Open();
                objCom.ExecuteScalar();
                objCon.Close();
                _retVal = "Sucess";

            }
            catch (Exception ex)
            {
                _retVal = ex.Message;
            }

            return _retVal;
        }

        public ImpactKPI FindImpactKPI(string strKPI)
        {
            ImpactKPI _retVal = new ImpactKPI();
            SqlConnection objCon = new SqlConnection();
            SqlCommand objCom;

            ConnectionString(objCon);
            objCom = new SqlCommand("SELECT intImpactKPIID, strKPI, strKPIDescr, dtmCreated, strCreatorID, dtmModified, strModifierID from GIOS.GIOS.GIOS_UImpactKPI where strKPI = @strKPI" , objCon);
            objCom.Parameters.Add(new SqlParameter("@strKPI", strKPI));
            objCon.Open();
            SqlDataReader reader = objCom.ExecuteReader();
            while (reader.Read())
            {
                _retVal.intImpactKPIID = Convert.ToInt32(reader["intImpactKPIID"]);
                _retVal.strKPI = reader["strKPI"].ToString();
                _retVal.strKPIDescr = reader["strKPIDescr"].ToString();
                _retVal.dtmCreated = Convert.ToDateTime(reader["dtmCreated"]);
                _retVal.strCreatorID = reader["strCreatorID"].ToString();
                if (reader["dtmModified"] == System.DBNull.Value) { _retVal.dtmModified = null; }
                else { _retVal.dtmModified = Convert.ToDateTime(reader["dtmModified"]); }
                _retVal.strModifierID = reader["strModifierID"].ToString();
            }
            reader.Close();
            objCon.Close();
            return _retVal;
        }



        public IEnumerable<string> GetUSeCaseImpactKPI(int intUseCaseID)
        {
            List<string> _retVal = new List<string>();
            SqlConnection objCon = new SqlConnection();
            SqlCommand objCom;
           // UseCaseImpactKPI useCaseImpactKPI;
            try
            {
                ConnectionString(objCon);
                objCom = new SqlCommand("Select strKPI from GIOS.GIOS_UUseCaseImpactKPI where intUseCaseID = @intUseCaseID", objCon);
                objCom.Parameters.Add(new SqlParameter("@intUseCaseID", intUseCaseID));

                objCon.Open();
                SqlDataReader reader = objCom.ExecuteReader();
                while (reader.Read())
                {
                    //  useCaseImpactKPI = new UseCaseImpactKPI();
                    //  useCaseImpactKPI.strKPI = reader["strKPI"].ToString();
                    string val = reader["strKPI"].ToString();
                    _retVal.Add(val);
                }

                reader.Close();
                objCon.Close();
                return _retVal;
            }catch(Exception ex)
            {
                return _retVal;
            }
        }

        public void RemoveUseCaseImpackKPI (int intUseCaseID)
        {
            SqlConnection objCon = new SqlConnection();
            SqlCommand objCom;
            try
            {
                ConnectionString(objCon);
                objCom = new SqlCommand("DELETE from GIOS.GIOS_UUseCaseImpactKPI where intUseCaseID = @intUseCaseID", objCon);
                objCom.Parameters.Add(new SqlParameter("@intUseCaseID", intUseCaseID));
                objCon.Open();

                objCom.ExecuteScalar();
                objCon.Close();
            }
            catch (Exception ex)
            {

            }
        }

        #endregion

        #region CREATE NEW USE CASE 
        public int CreateEditUseCase(string AddUpdateFlag, UseCase useCase)
        {
            int _retVal;
            SqlConnection objCon = new SqlConnection();
            SqlCommand objCom;
            try
            {
                ConnectionString(objCon);
                objCom = new SqlCommand("GIOS.GIOS_UCAddUpdateUseCase", objCon);
                objCom.CommandType = System.Data.CommandType.StoredProcedure;
                objCom.Parameters.Add(new SqlParameter("@AddUpdateFlag", AddUpdateFlag));
                objCom.Parameters.Add(new SqlParameter("@intUseCaseID", ToDBNull(useCase.intUseCaseID)));
                objCom.Parameters.Add(new SqlParameter("@strSmNumber", ToDBNull(useCase.strSMNumber)));
                objCom.Parameters.Add(new SqlParameter("@intDINumber", ToDBNull(useCase.intDINumber)));
                objCom.Parameters.Add(new SqlParameter("@intPlantID", ToDBNull(useCase.intPlantID)));
                objCom.Parameters.Add(new SqlParameter("@ProductProcessID", ToDBNull(useCase.ProductProcessID)));
                objCom.Parameters.Add(new SqlParameter("@strStatus", ToDBNull(useCase.Status)));
                objCom.Parameters.Add(new SqlParameter("@strCategoryID", "Asset Utilization"));
                objCom.Parameters.Add(new SqlParameter("@strUseCaseSPA", ToDBNull(useCase.strUseCaseSPA)));
                objCom.Parameters.Add(new SqlParameter("@strTitle", ToDBNull(useCase.strTitle)));
                objCom.Parameters.Add(new SqlParameter("@strDescription", ToDBNull(useCase.strDescription)));
                objCom.Parameters.Add(new SqlParameter("@strHypothesis", ToDBNull(useCase.strHypothesis)));
                objCom.Parameters.Add(new SqlParameter("@strAssests", ToDBNull(useCase.strAssests)));
                objCom.Parameters.Add(new SqlParameter("@strTeam", ToDBNull(useCase.strTeam)));
                objCom.Parameters.Add(new SqlParameter("@fltIdeaEstimate", ToDBNull(useCase.fltIdeaEstimate)));
                objCom.Parameters.Add(new SqlParameter("@strReqSupportOther", ToDBNull(useCase.strReqSupportOther)));
                objCom.Parameters.Add(new SqlParameter("@intCAPEXEstimate", ToDBNull(useCase.intCAPEXEstimate)));
                objCom.Parameters.Add(new SqlParameter("@ysnITAR", ToDBNull(useCase.ysnITAR)));
                objCom.Parameters.Add(new SqlParameter("@strAutomationCategory", ToDBNull(useCase.strAutomationCategory)));
                objCom.Parameters.Add(new SqlParameter("@strLessonsLearned", ToDBNull(useCase.strLessonsLearned)));
                objCom.Parameters.Add(new SqlParameter("@fltTotInvestment", ToDBNull(useCase.fltTotInvestment)));
                objCom.Parameters.Add(new SqlParameter("@strImpactCalculation", ToDBNull(useCase.strImpactCalculation)));
                objCom.Parameters.Add(new SqlParameter("@strTechnologyProvider", ToDBNull(useCase.strTechnologyProvider)));
                if (AddUpdateFlag == "I") { objCom.Parameters.Add(new SqlParameter("@strUserID", useCase.strCreatorID)); }
                else { objCom.Parameters.Add(new SqlParameter("@strUserID", useCase.strModifierID)); }

                objCon.Open();
               _retVal = (int)objCom.ExecuteScalar();
                objCon.Close();
            }
            catch (Exception ex)
            {
                _retVal = -1;
            }
            return _retVal;
        }

        public void CreateEditUseCaseImpactKPI(string AddUpdateFlag, UseCaseImpactKPI newUseCaseImpactKPI)
        {
            SqlConnection objCon = new SqlConnection();
            SqlCommand objCom;
            try
            {
                ConnectionString(objCon);
                objCom = new SqlCommand("GIOS.GIOS_UCAddUpdateUseCaseImpactKPI", objCon);
                objCom.CommandType = System.Data.CommandType.StoredProcedure;
                objCom.Parameters.Add(new SqlParameter("@AddUpdateFlag", AddUpdateFlag));
                objCom.Parameters.Add(new SqlParameter("@intUseCaseID", newUseCaseImpactKPI.intUseCaseID));
                objCom.Parameters.Add(new SqlParameter("@strKPI", newUseCaseImpactKPI.strKPI));
                if (AddUpdateFlag == "I") { objCom.Parameters.Add(new SqlParameter("@strUserID", newUseCaseImpactKPI.strCreatorID)); }
                else { objCom.Parameters.Add(new SqlParameter("@strUserID", newUseCaseImpactKPI.strModifierID)); }
                objCon.Open();

                objCom.ExecuteScalar();
                objCon.Close();
            }
            catch (Exception ex)
            {
               
            }
        }

        public void CreateEditUseCaseExtraSupport(string AddUpdateFlag, UseCaseExtraSupport newUseCaseExtraSupport)
        {
            SqlConnection objCon = new SqlConnection();
            SqlCommand objCom;
            try
            {
                ConnectionString(objCon);
                objCom = new SqlCommand("GIOS.GIOS_AddUpdateUseCaseExtraSupport", objCon);
                objCom.CommandType = System.Data.CommandType.StoredProcedure;
                objCom.Parameters.Add(new SqlParameter("@AddUpdateFlag", AddUpdateFlag));
                objCom.Parameters.Add(new SqlParameter("@intUseCaseID", newUseCaseExtraSupport.intUseCaseID));
                objCom.Parameters.Add(new SqlParameter("@strSupportType", newUseCaseExtraSupport.strSupportType));
                if (AddUpdateFlag == "I") { objCom.Parameters.Add(new SqlParameter("@strUserID", newUseCaseExtraSupport.strCreatorID)); }
                else { objCom.Parameters.Add(new SqlParameter("@strUserID", newUseCaseExtraSupport.strModifierID)); }
                objCon.Open();

                objCom.ExecuteScalar();
                objCon.Close();
            }
            catch (Exception ex)
            {

            }
        }

        public void UploadFile(UseCaseAttachments attachments)
        {
            SqlConnection objCon = new SqlConnection();
            SqlCommand objCom;
            try
            {
                ConnectionString(objCon);
                objCom = new SqlCommand("GIOS.GIOS_AddUseCaseAttachment", objCon);
                objCom.CommandType = System.Data.CommandType.StoredProcedure;
                objCom.Parameters.Add(new SqlParameter("@intUseCaseID", attachments.intUseCaseID));
                objCom.Parameters.Add(new SqlParameter("@Attachment", attachments.Attachment));
                objCom.Parameters.Add(new SqlParameter("@strFileType", attachments.strFileType));
                objCom.Parameters.Add(new SqlParameter("@strFileName", attachments.strFileName));
                objCom.Parameters.Add(new SqlParameter("@strUserID", attachments.strCreatorID));
                objCon.Open();

                objCom.ExecuteScalar();
                objCon.Close();
            }
            catch (Exception ex)
            {

            }
        }
    #endregion

        #region FILE TYPE FUNCTIONS
        public IEnumerable<FileTypes> ListFileTypes()
        {
            List<FileTypes> _retVal = new List<FileTypes>();
            SqlConnection objCon = new SqlConnection();
            SqlCommand objCom;
            FileTypes fileExts;

            ConnectionString(objCon);
            objCom = new SqlCommand("Select strFileType, strFileDescription, dteCreated, CreaterID, dteModified, ModifierID from GIOS.GIOS_SVC.FileTypes", objCon);

            objCon.Open();
            SqlDataReader reader = objCom.ExecuteReader();
            while (reader.Read())
            {
                fileExts = new FileTypes();
                fileExts.strFileType = reader["strFileType"].ToString();
                fileExts.strFileDescription = reader["strFileDescription"].ToString();
                fileExts.dtmCreated = Convert.ToDateTime(reader["dteCreated"]);
                fileExts.strCreatorID = reader["CreaterID"].ToString();
                if (reader["dteModified"] == System.DBNull.Value) { fileExts.dtmModified = null; }
                else { fileExts.dtmModified = Convert.ToDateTime(reader["dteModified"]); }
                fileExts.strModifierID = reader["ModifierID"].ToString();

                _retVal.Add(fileExts);
            }
            reader.Close();
            objCon.Close();
            return _retVal;
        }

        public string CreateEditFileType(string AddUpdateFlag, FileTypes fileTypes)
        {
            string _retVal;
            SqlConnection objCon = new SqlConnection();
            SqlCommand objCom;
            try
            {
                ConnectionString(objCon);

                objCom = new SqlCommand("GIOS.GIOS_AddUpdateFileType", objCon);
                objCom.CommandType = System.Data.CommandType.StoredProcedure;
                objCom.Parameters.Add(new SqlParameter("@AddupdateFlag", AddUpdateFlag));
                objCom.Parameters.Add(new SqlParameter("@strFileType", fileTypes.strFileType));
                objCom.Parameters.Add(new SqlParameter("@strFileDescription", fileTypes.strFileDescription));
                if (AddUpdateFlag == "I"){objCom.Parameters.Add(new SqlParameter("@strUserID", fileTypes.strCreatorID)); }
                else{ objCom.Parameters.Add(new SqlParameter("@strUserID", fileTypes.strModifierID)); }
                objCon.Open();
                objCom.ExecuteScalar();
                objCon.Close();
                _retVal = "Sucess";

            }
            catch (Exception ex)
            {
                _retVal = ex.Message;
            }

            return _retVal;
        }

        public FileTypes FindFileType(string id)
        {
            FileTypes _retVal = new FileTypes();
            SqlConnection objCon = new SqlConnection();
            SqlCommand objCom;

            ConnectionString(objCon);
            objCom = new SqlCommand("Select strFileType, strFileDescription, dteCreated, CreaterID, dteModified, ModifierID from GIOS.GIOS_SVC.FileTypes where strFileType='" + id + "'", objCon);

            objCon.Open();
            SqlDataReader reader = objCom.ExecuteReader();
            while (reader.Read())
            {
                _retVal.strFileType = reader["strFileType"].ToString();
                _retVal.strFileDescription = reader["strFileDescription"].ToString();
                _retVal.dtmCreated = Convert.ToDateTime(reader["dteCreated"]);
                _retVal.strCreatorID = reader["CreaterID"].ToString();
                if (reader["dteModified"] == System.DBNull.Value) { _retVal.dtmModified = null; }
                else { _retVal.dtmModified = Convert.ToDateTime(reader["dteModified"]); }
                _retVal.strModifierID = reader["ModifierID"].ToString();
            }
            reader.Close();
            objCon.Close();
            return _retVal;
        }
        #endregion

        #region Status Fucntions

        public IEnumerable<Status> ListStatuses()
        {
            List<Status> _retVal = new List<Status>();
            SqlConnection objCon = new SqlConnection();
            SqlCommand objCom;
            Status status;

            ConnectionString(objCon);
            objCom = new SqlCommand("Select strStatus, strDescription, yesActive, dteCreated, CreaterID, dteModified, ModifierID from GIOS.GIOS_SVC.Status", objCon);

            objCon.Open();
            SqlDataReader reader = objCom.ExecuteReader();
            while (reader.Read())
            {
                status = new Status();
                status.strStatus = reader["strStatus"].ToString();
                status.strDescription = reader["strDescription"].ToString();
                status.yesActive = Convert.ToBoolean(reader["yesActive"]);
                status.dtmCreated = Convert.ToDateTime(reader["dteCreated"]);
                status.strCreatorID = reader["CreaterID"].ToString();
                if (reader["dteModified"] == System.DBNull.Value) { status.dtmModified = null; }
                else { status.dtmModified = Convert.ToDateTime(reader["dteModified"]); }
                status.strModifierID = reader["ModifierID"].ToString();

                _retVal.Add(status);
            }
            reader.Close();
            objCon.Close();
            return _retVal;
        }

        public string CreateEditStatus(string AddUpdateFlag, Status status)
        {
            string _retVal;
            SqlConnection objCon = new SqlConnection();
            SqlCommand objCom;
            try
            {
                ConnectionString(objCon);

                objCom = new SqlCommand("GIOS.GIOS_AddUpdateStatus", objCon);
                objCom.CommandType = System.Data.CommandType.StoredProcedure;
                objCom.Parameters.Add(new SqlParameter("@AddupdateFlag", AddUpdateFlag));
                objCom.Parameters.Add(new SqlParameter("@strStatus", status.strStatus));
                objCom.Parameters.Add(new SqlParameter("@strDescription", status.strDescription));
                objCom.Parameters.Add(new SqlParameter("@yesActive", status.yesActive));
                if (AddUpdateFlag == "I") { objCom.Parameters.Add(new SqlParameter("@strUserID", status.strCreatorID)); }
                else { objCom.Parameters.Add(new SqlParameter("@strUserID", status.strModifierID)); }
                objCon.Open();
                objCom.ExecuteScalar();
                objCon.Close();
                _retVal = "Sucess";

            }
            catch (Exception ex)
            {
                _retVal = ex.Message;
            }

            return _retVal;
        }

        public Status FindStatus(string id)
        {
            Status _retVal = new Status();
            SqlConnection objCon = new SqlConnection();
            SqlCommand objCom;

            ConnectionString(objCon);
            objCom = new SqlCommand("Select strStatus, strDescription, yesActive, dteCreated, CreaterID, dteModified, ModifierID from GIOS.GIOS_SVC.Status where strStatus='" + id + "'", objCon);

            objCon.Open();
            SqlDataReader reader = objCom.ExecuteReader();
            while (reader.Read())
            {
               
                _retVal.strStatus = reader["strStatus"].ToString();
                _retVal.strDescription = reader["strDescription"].ToString();
                _retVal.yesActive = Convert.ToBoolean(reader["yesActive"]);
                _retVal.dtmCreated = Convert.ToDateTime(reader["dteCreated"]);
                _retVal.strCreatorID = reader["CreaterID"].ToString();
                if (reader["dteModified"] == System.DBNull.Value) { _retVal.dtmModified = null; }
                else { _retVal.dtmModified = Convert.ToDateTime(reader["dteModified"]); }
                _retVal.strModifierID = reader["ModifierID"].ToString();
                
            }
            reader.Close();
            objCon.Close();
            return _retVal;
        }


        #endregion

        #region Compass Cat Functions
        public IEnumerable<CompassCat> ListCompassCats()
        {
            List<CompassCat> _retVal = new List<CompassCat>();
            SqlConnection objCon = new SqlConnection();
            SqlCommand objCom;
            CompassCat compassCat;

            ConnectionString(objCon);
            objCom = new SqlCommand("Select strCategoryID, strCategoryDescr, dteCreated, CreaterID, dteModified, ModifierID from GIOS.GIOS_SVC.CompassCat", objCon);

            objCon.Open();
            SqlDataReader reader = objCom.ExecuteReader();
            while (reader.Read())
            {
                compassCat = new CompassCat();
                compassCat.strCategoryID = reader["strCategoryID"].ToString();
                compassCat.strCategoryDescr = reader["strCategoryDescr"].ToString();
                compassCat.dtmCreated = Convert.ToDateTime(reader["dteCreated"]);
                compassCat.strCreatorID = reader["CreaterID"].ToString();
                if (reader["dteModified"] == System.DBNull.Value) { compassCat.dtmModified = null; }
                else { compassCat.dtmModified = Convert.ToDateTime(reader["dteModified"]); }
                compassCat.strModifierID = reader["ModifierID"].ToString();

                _retVal.Add(compassCat);
            }
            reader.Close();
            objCon.Close();
            return _retVal;
        }

        public string CreateEditCompassCat(string AddUpdateFlag, CompassCat compassCat)
        {
            string _retVal;
            SqlConnection objCon = new SqlConnection();
            SqlCommand objCom;
            try
            {
                ConnectionString(objCon);

                objCom = new SqlCommand("GIOS.GIOS_AddUpdateCompassCat", objCon);
                objCom.CommandType = System.Data.CommandType.StoredProcedure;
                objCom.Parameters.Add(new SqlParameter("@AddupdateFlag", AddUpdateFlag));
                objCom.Parameters.Add(new SqlParameter("@strCategoryID", compassCat.strCategoryID));
                objCom.Parameters.Add(new SqlParameter("@strCategoryDescr", compassCat.strCategoryDescr));
                if (AddUpdateFlag == "I") { objCom.Parameters.Add(new SqlParameter("@strUserID", compassCat.strCreatorID)); }
                else { objCom.Parameters.Add(new SqlParameter("@strUserID", compassCat.strModifierID)); }
                objCon.Open();
                objCom.ExecuteScalar();
                objCon.Close();
                _retVal = "Sucess";

            }
            catch (Exception ex)
            {
                _retVal = ex.Message;
            }

            return _retVal;
        }

        public CompassCat FindCompassCat(string id)
        {
            CompassCat _retVal = new CompassCat();
            SqlConnection objCon = new SqlConnection();
            SqlCommand objCom;

            ConnectionString(objCon);
            objCom = new SqlCommand("Select strCategoryID, strCategoryDescr, dteCreated, CreaterID, dteModified, ModifierID from GIOS.GIOS_SVC.CompassCat where strCategoryID ='" + id + "'", objCon);

            objCon.Open();
            SqlDataReader reader = objCom.ExecuteReader();
            while (reader.Read())
            {
                _retVal = new CompassCat();
                _retVal.strCategoryID = reader["strCategoryID"].ToString();
                _retVal.strCategoryDescr = reader["strCategoryDescr"].ToString();
                _retVal.dtmCreated = Convert.ToDateTime(reader["dteCreated"]);
                _retVal.strCreatorID = reader["CreaterID"].ToString();
                if (reader["dteModified"] == System.DBNull.Value) { _retVal.dtmModified = null; }
                else { _retVal.dtmModified = Convert.ToDateTime(reader["dteModified"]); }
                _retVal.strModifierID = reader["ModifierID"].ToString();
            }
            reader.Close();
            objCon.Close();
            return _retVal;
        }

        #endregion

        #region Attachment Functions
        public IList<UseCaseAttachments> GetUseCaseAttachments(int intUseCaseID)
        {
            List<UseCaseAttachments> _retVal = new List<UseCaseAttachments>();
            SqlConnection objCon = new SqlConnection();
            SqlCommand objCom;
            UseCaseAttachments attachments;
            try
            {
                ConnectionString(objCon);
                objCom = new SqlCommand("Select lngRecordID, Attachment, strFileType, strFileName from GIOS.GIOS_UUseCaseAttachment where intUseCaseID = @intUseCaseID", objCon);
                objCom.Parameters.Add(new SqlParameter("@intUseCaseID", intUseCaseID));

                objCon.Open();
                SqlDataReader reader = objCom.ExecuteReader();
                while (reader.Read())
                {
                    attachments = new UseCaseAttachments();
                    attachments.lngRecordID = Convert.ToInt32(reader["lngRecordID"]);
                    attachments.strFileType = reader["strFileType"].ToString();
                    attachments.strFileName = reader["strFileName"].ToString();
                    attachments.Attachment = (byte[])reader["Attachment"];
                    _retVal.Add(attachments);
                }

                reader.Close();
                objCon.Close();
                return _retVal;
            }
            catch (Exception ex)
            {
                return _retVal;
            }
        }

        public UseCaseAttachments FindUseCaseAttachment (int lngRecordID)
        {
            //List<UseCaseAttachments> _retVal = new List<UseCaseAttachments>();
            SqlConnection objCon = new SqlConnection();
            SqlCommand objCom;
            UseCaseAttachments attachment = new UseCaseAttachments(); ;
            try
            {
                ConnectionString(objCon);
                objCom = new SqlCommand("Select  Attachment, strFileType, strFileName from GIOS.GIOS_UUseCaseAttachment where lngRecordID = @lngRecordID", objCon);
                objCom.Parameters.Add(new SqlParameter("@lngRecordID", lngRecordID));

                objCon.Open();
                SqlDataReader reader = objCom.ExecuteReader();
                while (reader.Read())
                { 
                    attachment.strFileType = reader["strFileType"].ToString();
                    attachment.strFileName = reader["strFileName"].ToString();
                    attachment.Attachment = (byte[])reader["Attachment"];
                    
                }

                reader.Close();
                objCon.Close();
                return attachment;
            }
            catch (Exception ex)
            {
                return attachment;
            }
        }

        public void DeleteUseCaseAttachment (int lngRecordID)
        {
            SqlConnection objCon = new SqlConnection();
            SqlCommand objCom;
            try
            {
                ConnectionString(objCon);
                objCom = new SqlCommand("GIOS.GIOS_UCDeleteUseCaseAttachment", objCon);
                objCom.CommandType = System.Data.CommandType.StoredProcedure;
                objCom.Parameters.Add(new SqlParameter("@lngRecordID", lngRecordID));
                objCon.Open();

                objCom.ExecuteScalar();
                objCon.Close();
            }
            catch (Exception ex)
            {

            }
        }
        #endregion

        #region Use Case Approval Functions

        public void CreateEditUseCaseApproval (string AddUpdateFlag , UseCaseApprovals useCaseApprovals)
        {
            string _retVal;
            SqlConnection objCon = new SqlConnection();
            SqlCommand objCom;
            try
            {
                ConnectionString(objCon);

                objCom = new SqlCommand("GIOS.GIOS_UCAddUpdateUseCaseApproval", objCon)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };
                objCom.Parameters.Add(new SqlParameter("@AddupdateFlag", AddUpdateFlag));
                objCom.Parameters.Add(new SqlParameter("@strBUID", useCaseApprovals.strBUID));
                objCom.Parameters.Add(new SqlParameter("@intUseCaseID", useCaseApprovals.intUseCaseID));
                objCom.Parameters.Add(new SqlParameter("@ysnApproved", ToDBNull(useCaseApprovals.ysnApproved)));
                objCom.Parameters.Add(new SqlParameter("@strComments", ToDBNull(useCaseApprovals.strComments)));
                if (AddUpdateFlag == "I"){objCom.Parameters.Add(new SqlParameter("@strUserID", useCaseApprovals.strCreatorID)); }
                else{objCom.Parameters.Add(new SqlParameter("@strUserID", useCaseApprovals.strApproverID));}
                objCon.Open();
                objCom.ExecuteScalar();
                objCon.Close();
                _retVal = "Sucess";

            }
            catch (Exception ex)
            {
                _retVal = ex.Message;
            }
        }

        public IEnumerable<UseCaseApprovals> ListUseCaseApprovals()
        {
            List<UseCaseApprovals> _retVal = new List<UseCaseApprovals>();
            SqlConnection objCon = new SqlConnection();
            SqlCommand objCom;
            UseCaseApprovals toBeApproved;

            ConnectionString(objCon);
            objCom = new SqlCommand("SELECT [UA].[lngRecordID],[UA].[intUseCaseID], [UC].[strTitle], [UC].[strDescription], [UC].[strHypothesis], [UA].[strBUID],[UA].[ysnApproved],[UA].[strComments],[UA].[dtmCreated],[UA].[strCreatorID],[UA].[dtmApproved],[UA].[strApproverID] " +
                "FROM [GIOS].[GIOS].[GIOS_UUseCaseApprovals] UA, GIOS.GIOS.GIOS_UUseCase UC where[UA].[intUseCaseID] = [UC].[intUseCaseID]", objCon);
           // objCom.Parameters.Add(new SqlParameter("@strUserName", strUserName));

            objCon.Open();
            SqlDataReader reader = objCom.ExecuteReader();
            while (reader.Read())
            {
                toBeApproved = new UseCaseApprovals();
                toBeApproved.lngRecordID = Convert.ToInt32(reader["lngRecordID"]);
                toBeApproved.intUseCaseID = Convert.ToInt32(reader["intUseCaseID"]);
                toBeApproved.strUseCaseTitle = reader["strTitle"].ToString();
                toBeApproved.strUseCaseDescr = reader["strDescription"].ToString();
                toBeApproved.strBUID = reader["strBUID"].ToString();
                 toBeApproved.ysnApproved =reader["ysnApproved"].ToString(); 
                toBeApproved.strComments = reader["strComments"].ToString();
                toBeApproved.strCreatorID = reader["strCreatorID"].ToString();
                if (reader["dtmCreated"] == System.DBNull.Value) { toBeApproved.dtmCreated = null; }
                else { toBeApproved.dtmCreated = Convert.ToDateTime(reader["dtmCreated"]); }
                toBeApproved.strApproverID = reader["strApproverID"].ToString();
                if (reader["dtmApproved"] == System.DBNull.Value) { toBeApproved.dtmApproved = null; }
                else { toBeApproved.dtmApproved = Convert.ToDateTime(reader["dtmApproved"]); }


                _retVal.Add(toBeApproved);
            }

            reader.Close();
            objCon.Close();
            return _retVal;

        }

        public string ApproveRejectUseCase(UseCaseApprovals useCaseApprovals)
        {
            string _retVal;
            SqlConnection objCon = new SqlConnection();
            SqlCommand objCom;
            try
            {
                ConnectionString(objCon);

                objCom = new SqlCommand("GIOS.GIOS_UCApproveRejectUseCase", objCon);
                objCom.CommandType = System.Data.CommandType.StoredProcedure;
                objCom.Parameters.Add(new SqlParameter("@intUseCaseID", useCaseApprovals.intUseCaseID));
                objCom.Parameters.Add(new SqlParameter("@ysnApproved", useCaseApprovals.ysnApproved));
                objCom.Parameters.Add(new SqlParameter("@strComments", useCaseApprovals.strComments));
                objCom.Parameters.Add(new SqlParameter("@strUserID", useCaseApprovals.strApproverID)); 
                objCon.Open();
                objCom.ExecuteScalar();
                objCon.Close();
                _retVal = "Success";

            }
            catch (Exception ex)
            {
                _retVal = ex.Message;
            }

            return _retVal;
        }

        public string UpdateUseCaseStatus(int intUseCaseID, string strStatus)
        {
            string _retVal;
            SqlConnection objCon = new SqlConnection();
            SqlCommand objCom;
            try
            {
                ConnectionString(objCon);

                objCom = new SqlCommand("UPDATE GIOS.GIOS_UUseCase SET  strStatus = @strStatus WHERE intUseCaseID = @intUseCaseID", objCon);
                objCom.Parameters.Add(new SqlParameter("@intUseCaseID", intUseCaseID));
                objCom.Parameters.Add(new SqlParameter("@strStatus", strStatus));
                objCon.Open();
                objCom.ExecuteScalar();
                objCon.Close();
                _retVal = "Success";

            }
            catch (Exception ex)
            {
                _retVal = ex.Message;
            }

            return _retVal;
        }

        #endregion  

        #region Retrieve Use Cases

        public Phase2_Update GetUseCase (int intUseCaseID)
        {
            Phase2_Update _retVal = new Phase2_Update()
            {
                RequiredSupportTypes = ListrequiredSupportTypes().ToList(),
                compassCats = ListCompassCats().ToList(),
                supportedFileTypes = ListFileTypes().ToList(),
                ImpactKPIs = ListImpactKPIs().ToList(),
                SelectedImpactKPIs = GetUSeCaseImpactKPI(intUseCaseID).ToList(),
                SelectedSupport = GetUseCaseExtraSupport(intUseCaseID).ToList(),
                useCaseAttachments = GetUseCaseAttachments(intUseCaseID).ToList(),
                 businessUnits = svc_DAL.ListBusinessUnits().ToList()
            };

            SqlConnection objCon = new SqlConnection();
            SqlCommand objCom;

            ConnectionString(objCon);
            objCom = new SqlCommand("GIOS.GIOS_GetUseCase", objCon);
            objCom.CommandType = System.Data.CommandType.StoredProcedure;
            objCom.Parameters.Add(new SqlParameter("@intUseCaseID", intUseCaseID));

            objCon.Open();
            SqlDataReader reader = objCom.ExecuteReader();
            while (reader.Read())
            {
                _retVal.intUseCaseID = Convert.ToInt32(reader["intUseCaseID"]);
              //  _retVal.strSMNumber = reader["strSMNumber"].ToString();
                _retVal.intPlantID = Convert.ToInt32(reader["intPlantId"]);
                _retVal.ProductProcessID = Convert.ToInt32(reader["ProductProcessID"]);
                _retVal.status = reader["strStatus"].ToString();
                _retVal.strCategoryID = reader["strCategoryID"].ToString();
                _retVal.strUseCaseSPA = reader["strUseCaseSPA"].ToString();
                _retVal.strTitle = reader["strTitle"].ToString();
                _retVal.strDescription = reader["strDescription"].ToString();
                _retVal.strHypothesis = reader["strHypothesis"].ToString();
                _retVal.strAssests = reader["strAssests"].ToString();
                _retVal.strTeam = reader["strTeam"].ToString();
                _retVal.fltIdeaEstimate = Convert.ToInt32(reader["fltIdeaEstimate"]);
                _retVal.strImpactCalculation = reader["strImpactCalculation"].ToString();
                _retVal.ysnITAR =Convert.ToBoolean(reader["ysnITAR"]);
                _retVal.strAutomationCategory = reader["strAutomationCategory"].ToString();
                _retVal.strReqSupportOther = reader["strRequiredSupportOther"].ToString();
                if (reader["intCAPEXEStimate"] == System.DBNull.Value) { _retVal.intCAPEXEstimate = null; }
                else { _retVal.intCAPEXEstimate = Convert.ToInt32(reader["intCAPEXEStimate"]); }
                _retVal.strLessonsLearned = reader["strLessonsLearned"].ToString();
                if(reader["fltTotInvestment"] == System.DBNull.Value) { _retVal.fltTotInvestment = null; }
                else { _retVal.fltTotInvestment = Convert.ToInt32(reader["fltTotInvestment"]); }
                if (reader["intDINumber"] == System.DBNull.Value) { _retVal.intDINumber = null; }
                else { _retVal.intDINumber = Convert.ToInt32(reader["intDINumber"]); }
                _retVal.strTechnologyProvider = reader["strTechnologyProvider"].ToString();
              //  _retVal. = Convert.ToDateTime(reader["dtmCreated"]);
                _retVal.strCreatorID = reader["strCreatorID"].ToString();
                if (reader["dtmModified"] == System.DBNull.Value) { _retVal.dtmModified = null; }
                else { _retVal.dtmModified = Convert.ToDateTime(reader["dtmModified"]); }
                _retVal.strModifierID = reader["strModifierID"].ToString();
            }
            reader.Close();
            objCon.Close();
            return _retVal;

        }

        public IEnumerable<UseCase> listUseCaseByUser(string strUserName)
        {
            List<UseCase> _retVal = new List<UseCase>();
            SqlConnection objCon = new SqlConnection();
            SqlCommand objCom;
            UseCase ownedUseCase;

            ConnectionString(objCon);
            objCom = new SqlCommand("select intUseCaseID, intDINumber, strStatus, strUseCaseSPA, strTitle, dtmCreated, strCreatorID, strModifierID, dtmModified from gios.GIOS_UUseCase where strCreatorID = @strUserName or strUseCaseSPA = @strUserName", objCon);
            objCom.Parameters.Add(new SqlParameter("@strUserName", strUserName));

            objCon.Open();
            SqlDataReader reader = objCom.ExecuteReader();
            while (reader.Read())
            {
                ownedUseCase = new UseCase();
                ownedUseCase.intUseCaseID = Convert.ToInt32(reader["intUseCaseID"]);
                if(reader["intDINumber"] == System.DBNull.Value){ ownedUseCase.intDINumber = null; }
                else{ ownedUseCase.intDINumber = Convert.ToInt32(reader["intDINumber"]); }
                ownedUseCase.Status = reader["strStatus"].ToString();
                ownedUseCase.strTitle = reader["strTitle"].ToString();
                ownedUseCase.strUseCaseSPA = reader["strUseCaseSPA"].ToString();
                ownedUseCase.strCreatorID = reader["strCreatorID"].ToString();
                ownedUseCase.dtmCreated = Convert.ToDateTime(reader["dtmCreated"]);
                ownedUseCase.strModifierID = reader["strModifierID"].ToString();
                if (reader["dtmModified"] == System.DBNull.Value) { ownedUseCase.dtmModified = null; }
                else { ownedUseCase.dtmModified = Convert.ToDateTime(reader["dtmModified"]); }


                _retVal.Add(ownedUseCase);
            }

            reader.Close();
            objCon.Close();
            return _retVal;
        }
        #endregion

        private static object ToDBNull(object value)
        {
            if (null != value)
                return value;
            return DBNull.Value;
        }

        private static void ConnectionString(SqlConnection objCon)
        {
            objCon.ConnectionString = "Data Source = NOATDC-D16SQC92\\VALCOMMON2016;Initial Catalog = GIOS; Persist Security Info=True;User ID=arconic_webservices;Password=Gioswebservice2018;";
        }
    }
}