// Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
using System.Collections.Generic;

public class Root
{
    public E e { get; set; }
    public A a { get; set; }
    public D d { get; set; }
    public string c { get; set; }
    public List<object> h { get; set; }
    public List<object> b { get; set; }
    public List<object> g { get; set; }
    public F f { get; set; }
    public I i { get; set; }
}


public class A
{
    public int c { get; set; }
    public string a { get; set; }
    public string b { get; set; }
    public string d { get; set; }
    public int e { get; set; }
    public string g { get; set; }
    public F f { get; set; }
    public int j { get; set; }
    public string h { get; set; }
    public int i { get; set; }
}

public class C
{
    public string c { get; set; }
    public int a { get; set; }
    public List<object> b { get; set; }
    public int e { get; set; }
    public string g { get; set; }
    public int d { get; set; }
    public int f { get; set; }
    public J j { get; set; }
    public H h { get; set; }
    public List<object> i { get; set; }
}

public class B
{
    public int a { get; set; }
    public int e { get; set; }
    public C c { get; set; }
    public B b { get; set; }
    public List<object> d { get; set; }
    public H h { get; set; }
    public int g { get; set; }
    public int f { get; set; }
    public int i { get; set; }
    public string j { get; set; }
}

public class H
{
    public int a { get; set; }
    public List<object> b { get; set; }
    public int e { get; set; }
    public D d { get; set; }
    public string j { get; set; }
    public string c { get; set; }
    public string h { get; set; }
    public int g { get; set; }
    public int f { get; set; }
    public int i { get; set; }
}

public class D
{
    public string e { get; set; }
    public string a { get; set; }
    public string d { get; set; }
    public string c { get; set; }
    public string h { get; set; }
    public B b { get; set; }
    public int g { get; set; }
    public List<object> f { get; set; }
    public int i { get; set; }
}

public class F
{
    public int c { get; set; }
    public string a { get; set; }
    public List<object> b { get; set; }
    public D d { get; set; }
    public string e { get; set; }
    public int g { get; set; }
    public int f { get; set; }
}

public class E
{
    public A a { get; set; }
    public List<List<object>> b { get; set; }
    public int e { get; set; }
    public List<object> d { get; set; }
    public int c { get; set; }
    public string h { get; set; }
    public int g { get; set; }
    public List<object> f { get; set; }
    public int i { get; set; }
}

public class J
{
    public int c { get; set; }
    public string a { get; set; }
    public string b { get; set; }
}

public class I
{
    public int a { get; set; }
    public string b { get; set; }
    public E e { get; set; }
    public string d { get; set; }
    public List<List<object>> c { get; set; }
    public H h { get; set; }
    public G g { get; set; }
    public F f { get; set; }
}

public class G
{
    public string e { get; set; }
    public string c { get; set; }
    public string a { get; set; }
    public int g { get; set; }
    public B b { get; set; }
    public string d { get; set; }
    public int f { get; set; }
}


