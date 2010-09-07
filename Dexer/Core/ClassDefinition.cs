﻿/* Dexer Copyright (c) 2010 Sebastien LEBRETON

Permission is hereby granted, free of charge, to any person obtaining
a copy of this software and associated documentation files (the
"Software"), to deal in the Software without restriction, including
without limitation the rights to use, copy, modify, merge, publish,
distribute, sublicense, and/or sell copies of the Software, and to
permit persons to whom the Software is furnished to do so, subject to
the following conditions:

The above copyright notice and this permission notice shall be
included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE. */

using System.Collections.Generic;
using Dexer.Metadata;
using System.Linq;

namespace Dexer.Core
{
    public class ClassDefinition : ClassReference, IMemberDefinition
	{
        public AccessFlags AccessFlags { get; set; }
        public ClassReference SuperClass { get; set; }
        public IList<ClassDefinition> InnerClasses { get; set; }
        public IList<ClassReference> Interfaces { get; set; }
        public string SourceFile { get; set; }
        public IList<Annotation> Annotations { get; set; }
        public IList<FieldDefinition> Fields { get; set; }
        public IList<MethodDefinition> Methods { get; set; }
        public new ClassDefinition Owner
        {
            get { return base.Owner as ClassDefinition; }
            set { base.Owner = value; }
        }

        internal ClassDefinition()
        {
            TypeDescriptor = TypeDescriptors.FullyQualifiedName;

            Interfaces = new List<ClassReference>();
            Annotations = new List<Annotation>();
            Fields = new List<FieldDefinition>();
            Methods = new List<MethodDefinition>();
            InnerClasses = new List<ClassDefinition>();
        }

        internal ClassDefinition(ClassReference cref)
            : this()
        {
            this.Fullname = cref.Fullname;
            this.Namespace = cref.Namespace;
            this.Name = cref.Name;
        }

        public IEnumerable<MethodDefinition> GetMethods(string name)
        {
            foreach (var mdef in Methods)
                if (mdef.Name == name)
                    yield return mdef;
        }

        public MethodDefinition GetMethod(string name)
        {
            return Enumerable.First(GetMethods(name));
        }

        #region " AccessFlags "
        public bool IsPublic {
            get { return (AccessFlags & AccessFlags.Public) != 0; }
            set { AccessFlags |= AccessFlags.Public; }
        }

        public bool IsPrivate {
            get { return (AccessFlags & AccessFlags.Private) != 0; }
            set { AccessFlags |= AccessFlags.Private; }
        }

        public bool IsProtected {
            get { return (AccessFlags & AccessFlags.Protected) != 0; }
            set { AccessFlags |= AccessFlags.Protected; }
        }

        public bool IsStatic {
            get { return (AccessFlags & AccessFlags.Static) != 0; }
            set { AccessFlags |= AccessFlags.Static; }
        }

        public bool IsFinal {
            get { return (AccessFlags & AccessFlags.Final) != 0; }
            set { AccessFlags |= AccessFlags.Final; }
        }

        public bool IsInterface {
            get { return (AccessFlags & AccessFlags.Interface) != 0; }
            set { AccessFlags |= AccessFlags.Interface; }
        }

        public bool IsAbstract {
            get { return (AccessFlags & AccessFlags.Abstract) != 0; }
            set { AccessFlags |= AccessFlags.Abstract; }
        }

        public bool IsSynthetic {
            get { return (AccessFlags & AccessFlags.Synthetic) != 0; }
            set { AccessFlags |= AccessFlags.Synthetic; }
        }

        public bool IsAnnotation
        {
            get { return (AccessFlags & AccessFlags.Annotation) != 0; }
            set { AccessFlags |= AccessFlags.Annotation; }
        }

        public bool IsEnum {
            get { return (AccessFlags & AccessFlags.Enum) != 0; }
            set { AccessFlags |= AccessFlags.Enum; }
        }
        #endregion

        #region " IEquatable "
        public bool Equals(ClassDefinition other)
        {
            // Should be enough (ownership)
            return base.Equals(other);
        }

        public override bool Equals(TypeReference other)
        {
            return (other is ClassDefinition)
                && this.Equals(other as ClassDefinition);
        }
        #endregion


    }
}