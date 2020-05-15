
using Gios_mvcSolution.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;


namespace Gios_mvcSolution.Areas.Training.Api
{
    public class TrainingServiceController : ApiController
    {
        private SVC repository;
        // GET: api/TraningService
        public HttpResponseMessage GetRolesDetails()
        {

            List<Role> resultRole = null;
            List<Competency> resultCompetency = null;

            try
            {
                System.Collections.Specialized.NameValueCollection queryStringParams = HttpContext.Current.Request.QueryString;
                if (string.IsNullOrEmpty(queryStringParams["option"]))
                {
                    //throw an error message to the client that the querystring parameters are incorrect                 
                    return Request.CreateResponse(HttpStatusCode.OK,new { error = "incorrect params" });                   
                }
                string option = queryStringParams["option"].ToLower();
                //check for sub module name, get the data from SP and convert to JSON.
                repository = new SVC();
                if (option == "role")
                {
                    resultRole = repository.GetRoleNames();
                    return Request.CreateResponse(HttpStatusCode.OK, new { data = resultRole });                   

                }
                else if (option == "competency")
                {
                    resultCompetency = repository.GetCompetencyNames();
                    return Request.CreateResponse(HttpStatusCode.OK, new { data = resultCompetency });
                }
                else
                {
                    //throw an message back to client invalid option
                    return Request.CreateResponse(HttpStatusCode.OK, new { error = "incorrect option" });
                }


            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.OK, new { error = "sorry an error occured" });
            }
        }
        [System.Web.Services.WebMethod]
        [HttpPost]
        public HttpResponseMessage StartSurvey(Survey survey)
        {
            try
            {
                //role id, compid, take as a guest, takent by
                repository = new SVC();
                survey = repository.StartSurvey(survey);
                return Request.CreateResponse(HttpStatusCode.OK, new { data = survey });
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.OK, new { error = "sorry an error occured" });
            }
        }
        [HttpPost]
        public HttpResponseMessage FetchSurveyHistory(Survey survey)
        {
            try
            {
                string userId = survey.TakenBy;
                var surveyObj = new Survey();
                repository = new SVC();
                var surveyList = new List<Survey>();
                surveyList = repository.FetchSurveyHistory(userId);
                return Request.CreateResponse(HttpStatusCode.OK, new { data = surveyList });
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.OK, new { error = "sorry an error occured" });
            }
        }
        public HttpResponseMessage FetchSurveyDetail(Survey survey)
        {
            try
            {
                int trainingId = survey.Id;
                repository = new SVC();
                survey = repository.FetchSurveyDetailById(survey);
                return Request.CreateResponse(HttpStatusCode.OK, new { data = survey });
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.OK, new { error = "sorry an error occured" });
            }
        }
        [HttpPost]
        public HttpResponseMessage GetRoleCompetencyDetailsToEdit(Role role)
        {
            //var roleDetails = new List<Role>();
            repository = new SVC();
            List<Competency> resultCompetency = null;
            List<Level> levelList = null;
            List<Role> roleList = null;
            //roleDetails = repository.GetRoleDetailsAdmin();
            //int roleId;
            //roleId=role.Id;
            roleList = repository.GetRoleCompetencyDetails(role);
            //resultCompetency = repository.GetRoleCompetencyDetails(roleId);
            levelList = repository.getLevelNames();
            return Request.CreateResponse(HttpStatusCode.OK, new { data1 = roleList, data2 = levelList });
        }

        public HttpResponseMessage UpdateSurvey([FromBody]Survey surveyObj, [FromUri]string action)
        {
            string level = string.Empty;
            var ques = new List<Question>();
            string Result = string.Empty;
            var competencyList = new List<Competency>();
            competencyList = surveyObj.CompetencyList;
            int trainingId = surveyObj.Id;
            int roleId = surveyObj.RoleId;
            repository = new SVC();
            string result = string.Empty;          
            try
            {
                if (action == "next")
                {
                    surveyObj = CalculateLevel(surveyObj, "competency");
                }
                else
                {
                    surveyObj = CalculateLevel(surveyObj, "role");
                }
                // send the result corresponding to the competency in case of next button click
                repository.UpdateSurvey(surveyObj);
                if (action == "continue")
                {
                    result = repository.updateSurveyResult(surveyObj);
                }

                return Request.CreateResponse(HttpStatusCode.OK, new { data = result });
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.OK, new { error = "sorry an error occured" });
            }
        }
        public HttpResponseMessage GetListofComp()
        {
            List<Competency> complist = null;

            try
            {
                //role id, compid, take as a guest, takent by
                SVC compdet = new SVC();
                complist = compdet.GetCompetencyNames();

                return Request.CreateResponse(HttpStatusCode.OK, new { data = complist });
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.OK, new { error = "sorry an error occured" });
            }
        }
        public HttpResponseMessage GetListofNewQues()
        {
            List<Question> queslist = null;

            try
            {
                //role id, compid, take as a guest, takent by
                repository = new SVC();
                queslist = repository.GetListofNewQues();

                return Request.CreateResponse(HttpStatusCode.OK, new { data = queslist });
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.OK, new { error = "sorry an error occured" });
            }
        }
        public HttpResponseMessage AddComp(Competency compobj)

        {
            try
            {
                List<Question> ques = new List<Question>();
                //new SelectListItem() { }
                string compname = compobj.Name;
                var questionList = new List<Question>();
                questionList = compobj.QuestionList;
                repository = new SVC();
                bool checkComp = repository.checkCompetencyName(compobj);
                if (checkComp)
                {
                    compobj = repository.AddComp(compobj);
                    return Request.CreateResponse(HttpStatusCode.OK, new { data = compobj, checkComp });

                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.OK, new { data = "", checkComp });
                }


            }

            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.OK, new { error = "sorry an error occured" });
            }
        }
        public HttpResponseMessage ViewComp()
        {
            List<Competency> complist = null;
            try
            {
                repository = new SVC();
                complist = repository.ViewComp();
                return Request.CreateResponse(HttpStatusCode.OK, new { data = complist});
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.OK, new { error = "sorry an error occured" });
            }
        }
        public HttpResponseMessage FetchCompetecyDetail(Competency Editobj)
        {
            List<Question> queslist = null;
            try
            {
                repository = new SVC();
                Editobj = repository.EditComp(Editobj);
                queslist = repository.GetListofNewQues();
                return Request.CreateResponse(HttpStatusCode.OK, new { competencyEditObject= Editobj ,questionList= queslist });
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.OK, new { error = "sorry an error occured" });
            }
        }
       
        public HttpResponseMessage SaveCompetencyDetail(Competency compObj)
        {
            try
            {
                List<Question> queslist = null;
                string compname = compObj.Name;
                queslist = compObj.QuestionList;
                repository = new SVC();
                bool checkUniqueComp = true;
                if (compObj.Id==-1)
                {
                    checkUniqueComp = repository.checkCompetencyName(compObj);
                }
                if (checkUniqueComp)
                {
                    compObj = repository.SaveCompetencyDetail(compObj);
                    return Request.CreateResponse(HttpStatusCode.OK, new { data = compObj, action = true, message = "" });
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.OK, new { data="",action=false,message = "Competency with same name already exists" });
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.OK, new { error = "sorry an error occured" });
            }
        }
        public HttpResponseMessage CompDelete(Competency compobj)
        {
            try
            {
                repository = new SVC();
                //int compid = compobj.Id;
                compobj = repository.DeleteComp(compobj);
                return Request.CreateResponse(HttpStatusCode.OK, new { data = compobj });
                // return Request.CreateResponse(HttpStatusCode.OK, new { error = "sorry an error occured" });
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.OK, new { error = "sorry an error occured" });
            }
        }
        public HttpResponseMessage GetRoleDetailsAdmin()
        {
            try
            {
                //role id, compid, take as a guest, takent by
                repository = new SVC();
                var role = new List<Role>();
                role = repository.GetRoleDetailsAdmin();
                return Request.CreateResponse(HttpStatusCode.OK, new { data = role });
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.OK, new { error = "sorry an error occured" });
            }
        }
        public HttpResponseMessage DeleteRoleDetail(Role role)
        {
            try
            {
                int roleId = role.Id;
                //var surveyObj = new Survey();
                repository = new SVC();
                bool result;
                result = repository.deleteRoleDetail(roleId);
                return Request.CreateResponse(HttpStatusCode.OK, new { data = result });
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.OK, new { error = "sorry an error occured" });
            }
        }

       
        public HttpResponseMessage GetCompetencyAndLevel()
        {

            //List<Role> resultRole = null;
            List<Competency> resultCompetency = null;
            List<Level> level = null;

            try
            {
                repository = new SVC();
                resultCompetency = repository.GetCompetencyNames();
                level = repository.getLevelNames();
                //return Request.CreateResponse(HttpStatusCode.OK, new { data = resultCompetency });
                return Request.CreateResponse(HttpStatusCode.OK, new { data1 = resultCompetency, data2 = level });

            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.OK, new { error = "sorry an error occured" });
            }
        }
        public HttpResponseMessage SaveRoleDetail(Role roleObj)
        {
            try
            {
                string roleName = roleObj.Name;
                //var surveyObj = new Survey();
                repository = new SVC();
                int result;
                bool uniqueFlag = true;
                if (roleObj.Id == -1)
                {
                    uniqueFlag = repository.checkUniqueRoleName(roleObj);
                }
                if (uniqueFlag)
                {
                    roleObj = repository.saveRoleDetails(roleObj);
                    return Request.CreateResponse(HttpStatusCode.OK, new { data = roleObj, action = uniqueFlag, message = "Data saved successfully" });
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.OK, new { data = roleObj, action = uniqueFlag, message = "Already data with the same name exists, please choose a new one" });
                }




            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.OK, new { error = "sorry an error occured" });
            }
        }
        public HttpResponseMessage GetCompetencyDeatilsTraining()
        {
            try
            {
                //role id, compid, take as a guest, takent by
                repository = new SVC();
                List<Competency> resultCompetency = null;
                resultCompetency = repository.GetCompetencyNames();
                return Request.CreateResponse(HttpStatusCode.OK, new { data = resultCompetency });
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.OK, new { error = "sorry an error occured" });
            }
        }


        #region Private Methods
        private Survey CalculateLevel(Survey surveyObj, string action)
        {
            bool? isRelevant;
            bool? answer;
            float surveyPercentage = 0;
            int relevantQuestionCountSurvey = 0;
            int relevantQuestionAffirmativeSurvey = 0;
            string surveyResult = "hi";
            int surveyLevId = -1;

            var competencyList = new List<Competency>();
            competencyList = surveyObj.CompetencyList;
            for (int j = 0; j < competencyList.Count; j++)
            {
                string competencyResult = "hi";
                int competencyLevId = -1;
                float competecyPercentage = 0;
                var competencyObj = new Competency();
                competencyObj = competencyList[j];
                int compId = competencyObj.Id;
                var competencyQuestionList = new List<Question>();
                competencyQuestionList = competencyObj.QuestionList;
                int LevId = -1;
                int relevantQuestionCountCompetency = 0;
                int relevantQuestionAffirmativeCompetency = 0;
                //calCulate overall percentage
                for (int n = 0; n < competencyQuestionList.Count; n++)
                {

                    isRelevant = competencyQuestionList[n].Relevant;
                    answer = competencyQuestionList[n].Answer;
                    if (isRelevant == true)
                    {
                        relevantQuestionCountSurvey++;
                        relevantQuestionCountCompetency++;
                        if (answer == true)
                        {
                            relevantQuestionAffirmativeSurvey++;
                            relevantQuestionAffirmativeCompetency++;
                        }
                    }
                }
                if (relevantQuestionCountCompetency != 0)
                {
                    competecyPercentage = ((float)relevantQuestionAffirmativeCompetency / relevantQuestionCountCompetency) * 100;

                }
                else
                {
                    competecyPercentage = 0;
                }
                if (competecyPercentage >= 85 && competecyPercentage <= 100)
                {
                    competencyResult = "Developer";
                    competencyLevId = 1;
                }
                if (competecyPercentage >= 50 && competecyPercentage < 85)
                {
                    competencyResult = "User";
                    competencyLevId = 2;
                }
                if (competecyPercentage < 50)
                {
                    competencyResult = "Aware";
                    competencyLevId = 3;
                }
                competencyList[j].LevelId = competencyLevId;
            }
            surveyObj.CompetencyList = competencyList;
            if (action == "role")
            {
                if (relevantQuestionCountSurvey != 0)
                {
                    surveyPercentage = ((float)relevantQuestionAffirmativeSurvey / relevantQuestionCountSurvey) * 100;
                }
                else
                {
                    surveyPercentage = 0;
                }
                if (surveyPercentage >= 85 && surveyPercentage <= 100)
                {
                    surveyResult = "Developer";
                    surveyLevId = 1;
                }
                if (surveyPercentage >= 50 && surveyPercentage < 85)
                {
                    surveyResult = "User";
                    surveyLevId = 2;
                }
                if (surveyPercentage < 50)
                {
                    surveyResult = "Aware";
                    surveyLevId = 3;
                }
                surveyObj.LevelId = surveyLevId;
            }
            return surveyObj;
        }

       

        #endregion
        // GET: api/TraningService/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/TraningService
        //public void Post([FromBody]string value)
        //{
        //}

        // PUT: api/TraningService/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/TraningService/5
        public void Delete(int id)
        {
        }
    }
}
