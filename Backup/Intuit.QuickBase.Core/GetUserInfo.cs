﻿/*
 * Copyright © 2013 Intuit Inc. All rights reserved.
 * All rights reserved. This program and the accompanying materials
 * are made available under the terms of the Eclipse Public License v1.0
 * which accompanies this distribution, and is available at
 * http://www.opensource.org/licenses/eclipse-1.0.php
 */
using System.Xml.XPath;
using Intuit.QuickBase.Core.Payload;
using Intuit.QuickBase.Core.Uri;

namespace Intuit.QuickBase.Core
{
    public class GetUserInfo : IQObject
    {
        private const string QUICKBASE_ACTION = "API_GetUserInfo";
        private Payload.Payload _getUserInfoPayload;
        private IQUri _uri;

        public GetUserInfo(string ticket, string appToken, string accountDomain, string email)
        {
            CommonConstruction(ticket, appToken, accountDomain, new GetUserInfoPayload(email));
        }

        public GetUserInfo(string ticket, string appToken, string accountDomain)
        {
            CommonConstruction(ticket, appToken, accountDomain, new GetUserInfoPayload());
        }

        private void CommonConstruction(string ticket, string appToken, string accountDomain, Payload.Payload payload)
        {
            _getUserInfoPayload = new ApplicationTicket(payload, ticket);
            _getUserInfoPayload = new ApplicationToken(_getUserInfoPayload, appToken);
            _getUserInfoPayload = new WrapPayload(_getUserInfoPayload);
            _uri = new QUriMain(accountDomain);
        }

        public string XmlPayload
        {
            get
            {
                return _getUserInfoPayload.GetXmlPayload();
            }
        }

        public System.Uri Uri
        {
            get
            {
                return _uri.GetQUri();
            }
        }

        public string Action
        {
            get
            {
                return QUICKBASE_ACTION;
            }
        }

        public XPathDocument Post()
        {
            HttpPost httpXml = new HttpPostXml();
            httpXml.Post(this);
            return httpXml.Response;
        }
    }
}
