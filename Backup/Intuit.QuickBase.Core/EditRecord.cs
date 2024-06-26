﻿/*
 * Copyright © 2013 Intuit Inc. All rights reserved.
 * All rights reserved. This program and the accompanying materials
 * are made available under the terms of the Eclipse Public License v1.0
 * which accompanies this distribution, and is available at
 * http://www.opensource.org/licenses/eclipse-1.0.php
 */
using System;
using System.Collections.Generic;
using System.Xml.XPath;
using Intuit.QuickBase.Core.Payload;
using Intuit.QuickBase.Core.Uri;

namespace Intuit.QuickBase.Core
{
    /// <summary>
    /// You can use this to change any of the editable field values in the specified record. Only those fields specified 
    /// are changed; unspecified fields are left unchanged. Unsupported tags: &lt;update_id&gt;&lt;/update_id&gt;, 
    /// &lt;field=""&gt;&lt;/field&gt;, &lt;disprec&gt;&lt;/disprec&gt;, &lt;fform&gt;&lt;/fform&gt;, 
    /// &lt;ignoreError&gt;&lt;/ignoreError&gt; and &lt;udata&gt;&lt;/udata&gt;
    /// </summary>
    public class EditRecord : IQObject
    {
        private const string QUICKBASE_ACTION = "API_EditRecord";
        private readonly Payload.Payload _editRecordPayload;
        private readonly IQUri _uri;

        public class Builder
        {
            private List<IField> _fields;
            private int _rid;

            internal string Ticket { get; set; }
            internal string AppToken { get; set; }
            internal string AccountDomain { get; set; }
            internal string Dbid { get; set; }
            internal int Rid
            {
                get
                {
                    return _rid;
                }
                set
                {
                    if (value < 1) throw new ArgumentException("rid");
                    _rid = value;
                }
            }
            internal List<IField> Fields
            {
                get
                {
                    return _fields;
                }
                set
                {
                    if (value == null) throw new ArgumentNullException("fields");
                    _fields = value;
                }
            }

            public Builder(string ticket, string appToken, string accountDomain, string dbid, int rid, List<IField> fields)
            {
                Ticket = ticket;
                AppToken = appToken;
                AccountDomain = accountDomain;
                Dbid = dbid;
                Rid = rid;
                Fields = fields;
            }

            internal string UpdateId { get; private set; }

            public Builder SetUpdateId(string val)
            {
                UpdateId = val;
                return this;
            }

            internal bool Disprec { get; private set; }

            public Builder SetDisprec(bool val)
            {
                Disprec = val;
                return this;
            }

            internal bool Fform { get; private set; }

            public Builder SetFform(bool val)
            {
                Fform = val;
                return this;
            }

            public EditRecord Build()
            {
                return new EditRecord(this);
            }
        }

        private EditRecord(Builder builder)
        {
            _editRecordPayload = new EditRecordPayload.Builder(builder.Rid, builder.Fields)
                .SetUpdateId(builder.UpdateId)
                .SetDisprec(builder.Disprec)
                .SetFform(builder.Fform)
                .Build();
            _editRecordPayload = new ApplicationTicket(_editRecordPayload, builder.Ticket);
            _editRecordPayload = new ApplicationToken(_editRecordPayload, builder.AppToken);
            _editRecordPayload = new WrapPayload(_editRecordPayload);
            _uri = new QUriDbid(builder.AccountDomain, builder.Dbid);
        }

        public string XmlPayload
        {
            get
            {
                return _editRecordPayload.GetXmlPayload();
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