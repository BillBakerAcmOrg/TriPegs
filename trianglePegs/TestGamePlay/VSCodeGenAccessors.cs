﻿// ------------------------------------------------------------------------------
//<autogenerated>
//        This code was generated by Microsoft Visual Studio Team System 2005.
//
//        Changes to this file may cause incorrect behavior and will be lost if
//        the code is regenerated.
//</autogenerated>
//------------------------------------------------------------------------------
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestGamePlay
{
[System.Diagnostics.DebuggerStepThrough()]
[System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.TestTools.UnitTestGeneration", "1.0.0.0")]
internal class BaseAccessor {
    
    protected Microsoft.VisualStudio.TestTools.UnitTesting.PrivateObject m_privateObject;
    
    protected BaseAccessor(object target, Microsoft.VisualStudio.TestTools.UnitTesting.PrivateType type) {
        m_privateObject = new Microsoft.VisualStudio.TestTools.UnitTesting.PrivateObject(target, type);
    }
    
    protected BaseAccessor(Microsoft.VisualStudio.TestTools.UnitTesting.PrivateType type) : 
            this(null, type) {
    }
    
    internal virtual object Target {
        get {
            return m_privateObject.Target;
        }
    }
    
    public override string ToString() {
        return this.Target.ToString();
    }
    
    public override bool Equals(object obj) {
        if (typeof(BaseAccessor).IsInstanceOfType(obj)) {
            obj = ((BaseAccessor)(obj)).Target;
        }
        return this.Target.Equals(obj);
    }
    
    public override int GetHashCode() {
        return this.Target.GetHashCode();
    }
}


[System.Diagnostics.DebuggerStepThrough()]
[System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.TestTools.UnitTestGeneration", "1.0.0.0")]
internal class trianglePegs_gameAccessor : BaseAccessor {
    
    protected static Microsoft.VisualStudio.TestTools.UnitTesting.PrivateType m_privateType = new Microsoft.VisualStudio.TestTools.UnitTesting.PrivateType(typeof(global::trianglePegs.game));
    
    internal trianglePegs_gameAccessor(global::trianglePegs.game target) : 
            base(target, m_privateType) {
    }
    
    internal bool[] board {
        get {
            bool[] ret = ((bool[])(m_privateObject.GetField("board")));
            return ret;
        }
        set {
            m_privateObject.SetField("board", value);
        }
    }
    
    internal global::System.Collections.Stack pastMoves {
        get {
            global::System.Collections.Stack ret = ((global::System.Collections.Stack)(m_privateObject.GetField("pastMoves")));
            return ret;
        }
        set {
            m_privateObject.SetField("pastMoves", value);
        }
    }
    
    internal static int[,] legalMoves {
        get {
            int[,] ret = ((int[,])(m_privateType.GetStaticField("legalMoves")));
            return ret;
        }
        set {
            m_privateType.SetStaticField("legalMoves", value);
        }
    }
    
    internal global::System.Collections.Specialized.ListDictionary jumpedSpace {
        get {
            global::System.Collections.Specialized.ListDictionary ret = ((global::System.Collections.Specialized.ListDictionary)(m_privateObject.GetField("jumpedSpace")));
            return ret;
        }
        set {
            m_privateObject.SetField("jumpedSpace", value);
        }
    }
    
    internal bool IsValidMove(int oldPosition, int newPosition) {
        object[] args = new object[] {
                oldPosition,
                newPosition};
        bool ret = ((bool)(m_privateObject.Invoke("IsValidMove", new System.Type[] {
                    typeof(int),
                    typeof(int)}, args)));
        return ret;
    }
    
    internal bool IsLegalMove(int oldPosition, int newPosition) {
        object[] args = new object[] {
                oldPosition,
                newPosition};
        bool ret = ((bool)(m_privateObject.Invoke("IsLegalMove", new System.Type[] {
                    typeof(int),
                    typeof(int)}, args)));
        return ret;
    }
    
    internal int JumpedSpace(int oldPosition, int newPosition) {
        object[] args = new object[] {
                oldPosition,
                newPosition};
        int ret = ((int)(m_privateObject.Invoke("JumpedSpace", new System.Type[] {
                    typeof(int),
                    typeof(int)}, args)));
        return ret;
    }
}
}
