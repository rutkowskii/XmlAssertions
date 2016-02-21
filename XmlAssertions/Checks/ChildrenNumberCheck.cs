﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace XmlAssertions.Checks
{
    internal class ChildrenNumberCheck
    {
        private readonly AssertContext _assertContext;

        public ChildrenNumberCheck(AssertContext assertContext)
        {
            _assertContext = assertContext;
        }

        public void AssertChildrenNumber(XmlNode expected)
        {
            var childrenActual = XmlUtils.ExtractChildNodes(_assertContext.XmlNode);
            var childrenExpected = XmlUtils.ExtractChildNodes(expected);
            var equalChildrenNumber = childrenActual.Count == childrenExpected.Count;
            if (!equalChildrenNumber)
            {
                _assertContext.ThrowErrorMessage(string.Format("Number of children is " +
                                                               "incorrent, expected [{0}], but was [{1}]",
                    childrenExpected.Count,
                    childrenActual.Count));
            }
        }

    }
}