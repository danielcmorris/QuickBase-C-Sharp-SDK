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
    internal class GrantDBsPayload : Payload
    {
        private readonly bool _excludeParents;
        private readonly bool _withEmbeddedTables;
        private readonly bool _adminOnly;

        internal class Builder
        {
            internal bool ExcludedParents { get; private set; }
            internal Builder SetExcludedParents(bool val)
            {
                ExcludedParents = val;
                return this;
            }

            internal bool WithEmbeddedTables { get; private set; }
            internal Builder SetWithEmbeddedTables(bool val)
            {
                WithEmbeddedTables = val;
                return this;
            }

            internal bool AdminOnly { get; private set; }
            internal Builder SetAdminOnly(bool val)
            {
                AdminOnly = val;
                return this;
            }

            internal GrantDBsPayload Build()
            {
                return new GrantDBsPayload(this);
            }
        }

        private GrantDBsPayload(Builder builder)
        {
            _excludeParents = builder.ExcludedParents;
            _withEmbeddedTables = builder.WithEmbeddedTables;
            _adminOnly = builder.AdminOnly;
        }

        internal override string GetXmlPayload()
        {
            var sb = new StringBuilder();
            sb.Append(_excludeParents ? "<Excludeparents>1</Excludeparents>" : "<Excludeparents>0</Excludeparents>");
            sb.Append(_withEmbeddedTables ? "<withembeddedtables>1</withembeddedtables>" : "<withembeddedtables>0</withembeddedtables>");
            sb.Append(_adminOnly ? "<adminOnly/>" : String.Empty);
            return sb.ToString();
        }
    }
}
