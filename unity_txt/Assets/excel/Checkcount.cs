using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Checkcount
{
    Dictionary<double,int> timecount = new Dictionary<double,int>();
      Dictionary<double,double> timecountlabel = new Dictionary<double,double>();
       countvalue Countvalue = new countvalue();
       int i = -10;
public void Main(double[] args)
{
	
ArrayList al2= Check(args);
Debug.Log(args.Length);
ArrayList al3 = new ArrayList();
foreach (double i in al2)
{
int times = Times(i,args);
for(int t=0;t<times;t++)
{
al3.Add(i);
Debug.Log(i +":"+times);
}
timecount.Add(i,times);
}
foreach (double i in al3)
{

Debug.Log("al3:"+i);
}

foreach (int k in timecount.Keys){
  i++;
    int value = timecount[k];
  double keyvalue =  (double)((double)value/(double)args.Length) * 100;
    timecountlabel.Add(k,keyvalue); 
    Debug.Log("占比"+k+":"+keyvalue);
    Countvalue.copyinit((float)keyvalue,i,k.ToString());

}
}
 ArrayList Check(double[] arr)
{
ArrayList al = new ArrayList();
for (int i = 0; i < arr.Length; i++)
{
for (int j = arr.Length - 1; j > i; j--)
{
if (arr[i] == arr[j])
{
if(!al.Contains(arr[i]))
{
al.Add(arr[i]);
}

}
}
}

return al;
}
 int Times(double s,double[] arry) //记录次数
{
int times=0;
for (int i = 0; i < arry.Length; i++) 
{
if (arry[i] == s) 
{
times++;
}
}
Debug.Log("times"+times);
return times;

}
}
