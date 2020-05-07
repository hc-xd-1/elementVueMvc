using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using StackExchange.Redis;
using StackExchange.Redis.KeyspaceIsolation;
using StackExchange.Redis.Profiling;
using System.Web.Script.Serialization;

namespace ElemnetUi_Vue.JS_Mvc.BLL
{
    public class RedisHelper
    {
       
        public static void InitAndGetAllKeys()
        {
            string errorMsg = string.Empty;
            List<Models.UserInfoModel> listUsers=SqlModelHelp.GetAllUsers(out errorMsg);
            using (var redis = ConnectionMultiplexer.Connect("Localhost"))
            {
                JavaScriptSerializer json = new JavaScriptSerializer();
                var db = redis.GetDatabase();
                string userJson=json.Serialize(listUsers);
                db.StringSet(new RedisKey("allUsers"), new RedisValue(userJson));
            }
        }


        public static void GetAllUserKeys(out List<Models.UserInfoModel> listUsers)
        {
            listUsers = new List<Models.UserInfoModel>();
            try
            {
                using (var redis = ConnectionMultiplexer.Connect("Localhost"))
                {
                    var db = redis.GetDatabase();
                    string userJson = db.StringGet(new RedisKey("allUsers"));
                    JavaScriptSerializer json = new JavaScriptSerializer();
                    listUsers = json.Deserialize<List<Models.UserInfoModel>>(userJson);                    
                }
            }
            catch(Exception ex)
            {

            }
        }

        public static void AddKey(string redisKey,string redisValue)
        {
            try
            {
                using (var redis = ConnectionMultiplexer.Connect("Localhost"))
                {
                    var db = redis.GetDatabase();
                    db.StringAppend(new RedisKey(redisKey), new RedisValue(redisValue));
                }
            }
            catch(Exception ex)
            {

            }
        }

        public static void DeleteKey(string redisKey)
        {
            try
            {
                using (var redis = ConnectionMultiplexer.Connect("Localhost"))
                {
                    var db = redis.GetDatabase();
                    db.KeyDelete(new RedisKey(redisKey));
                }
            }
            catch(Exception ex)
            {

            }
        }        
    }
}