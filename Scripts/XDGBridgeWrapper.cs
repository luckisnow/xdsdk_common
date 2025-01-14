using System.Collections.Generic;
using TapTap.Common;
using UnityEngine;
using XD.SDK.Common.Internal;

namespace XD.SDK.Common{
    public class XDGInitResultWrapper{
        public bool isSuccess;
        public string message;
        public LocalConfigInfo localConfigInfo;

        public XDGInitResultWrapper(string resultJson){
            var dic = Json.Deserialize(resultJson) as Dictionary<string, object>;
            isSuccess = SafeDictionary.GetValue<bool>(dic, "success");
            message = SafeDictionary.GetValue<string>(dic, "message");
            var configInfoDic = SafeDictionary.GetValue<Dictionary<string, object>>(dic, "configInfo");
            localConfigInfo = new LocalConfigInfo(configInfoDic);

            if (!isSuccess){
                XDGTool.LogError("初始化失败2 result json ：" + resultJson);
            }
        }
    }

    public class XDGShareResultWrapper{
        public bool cancel;

        public XDGError error;

        public XDGShareResultWrapper(string json){
            Dictionary<string, object> dic = Json.Deserialize(json) as Dictionary<string, object>;
            cancel = SafeDictionary.GetValue<bool>(dic, "cancel");
            Dictionary<string, object> errorDic = SafeDictionary.GetValue<Dictionary<string, object>>(dic, "error");
            if (errorDic != null){
                error = new XDGError(errorDic);
            }
        }

        public string ToJSON(){
            return JsonUtility.ToJson(this);
        }
    }

    public class XDGRegionInfoWrapper : IXDGRegionInfoWrapper{
        private XDGRegionInfo _info;

        public XDGRegionInfoWrapper(string json){
            var dic = Json.Deserialize(json) as Dictionary<string, object>;
            var infoDic = SafeDictionary.GetValue<Dictionary<string, object>>(dic, "info");
            _info = new XDGRegionInfo(infoDic);
        }

        public IXDGRegionInfo info => _info;
    }
    
    public class XDGAgreement : IXDGAgreement
    {
        private string _type;
        private string _url;
        
        public XDGAgreement(Dictionary<string, object> dic){
            if (dic == null) return;
            _type = SafeDictionary.GetValue<string>(dic, "type");
            _url = SafeDictionary.GetValue<string>(dic, "url");
        }

        public override string ToString()
        {
            return $"[type:{type};url:{url}]";
        }

        public string type => _type;
        public string url => _url;
    }
}