﻿/*
 * Copyright © 2010 Intuit Inc. All rights reserved.
 * All rights reserved. This program and the accompanying materials
 * are made available under the terms of the Eclipse Public License v1.0
 * which accompanies this distribution, and is available at
 * http://www.opensource.org/licenses/eclipse-1.0.php
 */
using System.Xml.XPath;

namespace Intuit.QuickBase.Core
{
    public interface IQGetObject
    {
        void AddParameter(string name, string value);
        System.Uri Uri { get; }
        string Action { get; }
        XPathDocument Get();
    }
}