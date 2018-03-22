using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;    
using System.ComponentModel;  
using System.Data;  
//using System.Drawing;  
using System.Linq;  
using System.Text;  
using System.IO; 
//using System.Windows.Forms;  
using System.Data.OleDb;  
  
namespace loading 
{  
    public partial class Form1 //: Form  
    {  
        public Form1()  
        {  
           // InitializeComponent();  
        }  
  
        private void button1_Click(object sender, EventArgs e)  
        {  
            string strConn = "Provider=Microsoft.Ace.OleDb.12.0;" + "data source=C:\\Users\\WaveMaker\\Desktop\\solar_m_kks_code.xlsx;Extended Properties='Excel 12.0; HDR=Yes; IMEX=1'";   
            OleDbConnection conn = new OleDbConnection(strConn); 
            conn.Open();  
            string strExcel = "";   
            OleDbDataAdapter myCommand = null;   
            DataTable dt = null;  
            strExcel = "select * from [sheet1$]";   
            myCommand = new OleDbDataAdapter(strExcel, strConn); dt = new DataTable();   
            myCommand.Fill(dt);  
            List<string> list = new List<string>();  
            for (int i = 0; i < dt.Rows.Count;i++ )  
            {  
                list.Add(dt.Rows[i][0].ToString());  
            }  
            //list.Add("dasd");  
        }  
    }  
}  
