﻿/*
 * Copyright © 2010 Intuit Inc. All rights reserved.
 * All rights reserved. This program and the accompanying materials
 * are made available under the terms of the Eclipse Public License v1.0
 * which accompanies this distribution, and is available at
 * http://www.opensource.org/licenses/eclipse-1.0.php
 */
using System;
using System.Text;

namespace Intuit.QuickBase.Core.Payload
{
    internal class ImportFromCSVPayload : Payload
    {
        private readonly string _recordsCsv;
        private readonly string _cList;
        private readonly bool _skipFirst;

        internal class Builder
        {
            internal Builder(string recordsCsv)
            {
                RecordsCsv = recordsCsv;
            }

            internal string RecordsCsv { get; set; }

            internal string CList { get; private set; }
            internal Builder SetCList(string val)
            {
                CList = val;
                return this;
            }

            internal bool SkipFirst { get; private set; }
            internal Builder SetSkipFirst(bool val)
            {
                SkipFirst = val;
                return this;
            }

            internal ImportFromCSVPayload Build()
            {
                return new ImportFromCSVPayload(this);
            }
        }

        private ImportFromCSVPayload(Builder builder)
        {
            _recordsCsv = builder.RecordsCsv;
            _cList = builder.CList;
            _skipFirst = builder.SkipFirst;
        }

        internal override string GetXmlPayload()
        {
            var sb = new StringBuilder();
            sb.Append(String.Format("<records_csv><![CDATA[{0}]]></records_csv>", _recordsCsv));
            sb.Append((!String.IsNullOrEmpty(_cList)) ? String.Format("<clist>{0}</clist>", _cList) : String.Empty);
            sb.Append(_skipFirst ? "<skipfirst>1</skipfirst>" : String.Empty);
            return sb.ToString();
        }
    }
}
