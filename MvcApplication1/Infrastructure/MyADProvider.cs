using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace CloudScheduler.Infrastructure
{
    public class MyADProvider : ActiveDirectoryMembershipProvider
    {
        public string ConnectionStringName { get; set; }
        public string AttributeMapUsername { get; set; }

        public override void Initialize(string name, System.Collections.Specialized.NameValueCollection config)
        {
            ConnectionStringName = config["connectionStringName"];
            AttributeMapUsername = config["attributeMapUsername"];

            base.Initialize(name, config);
        }
    }
}
