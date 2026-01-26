using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web.ASPxGridView;
using DevExpress.XtraPrinting;
using DevExpress.XtraPrintingLinks;
using System.IO;
using System.Data;
using DevExpress.Web.ASPxGridView;

/// <summary>
/// Summary description for gridview
/// </summary>
public class gridview
{
    public DevExpress.Web.ASPxGridView.GridViewDataDateColumn _AddColumnD(string _FieldName, string _Caption, string _format, bool _visible, int _width)
    {
        DevExpress.Web.ASPxGridView.GridViewDataDateColumn gc_ = new GridViewDataDateColumn();
        gc_ = new GridViewDataDateColumn();
        gc_.FieldName = _FieldName;
        gc_.Caption = _Caption;
        gc_.ReadOnly = true;
        gc_.Visible = _visible;

        gc_.Width = System.Web.UI.WebControls.Unit.Pixel(_width);
        gc_.PropertiesDateEdit.Width = System.Web.UI.WebControls.Unit.Pixel(_width + _width + _width);
        gc_.ExportWidth = _width;
        if (_format != "")
        {
            gc_.PropertiesDateEdit.DisplayFormatString = _format;
            gc_.PropertiesDateEdit.EditFormatString = _format;
        }
        //    grd_Browse1.Columns.Add(gc_);
        return gc_;
    }
    public GridViewDataTextColumn _AddColumn(string _FieldName, string _Caption, string _format, bool _visible)
    {//header ucun
        GridViewDataTextColumn gc_ = new GridViewDataTextColumn();

        gc_.FieldName = _FieldName;
        gc_.Caption = _Caption;
        gc_.ReadOnly = true;
        gc_.Visible = _visible;
        if (_format != "")
        {
            gc_.PropertiesTextEdit.DisplayFormatString = _format;
        }
        // grd_Browse1.Columns.Add(gc_);
        return gc_;
    }
    //protected string grid_selected_row(string _key)
    //{
    //    //setr secmek ucun
    //    string selected_value = "0";
    //    List<object> l = grd_Browse1.GetSelectedFieldValues(_key);//_KeyFieldName.value);

    //    if (l.Count > 0)
    //    {
    //        foreach (object id in l)
    //        {
    //            selected_value = id.ToString();
    //        }

    //    }
    //    return selected_value;
    //}
    public GridViewDataTextColumn _AddColumn(string _FieldName, string _Caption, string _format, bool _visible, int _width)
    {//header ucun
        GridViewDataTextColumn gc_ = new GridViewDataTextColumn();

        gc_.FieldName = _FieldName;
        gc_.Caption = _Caption;
        gc_.ReadOnly = true;
        gc_.Visible = _visible;
        gc_.Settings.AutoFilterCondition = AutoFilterCondition.Contains;
        gc_.Width = System.Web.UI.WebControls.Unit.Pixel(_width);
        gc_.PropertiesTextEdit.Width = System.Web.UI.WebControls.Unit.Pixel(_width);
        gc_.ExportWidth = _width;
        if (_format != "")
        {
            gc_.PropertiesTextEdit.DisplayFormatString = _format;
        }
        // grd_Browse1.Columns.Add(gc_);

        return gc_;
    }



    public GridViewBandColumn _AddColumnB(string _FieldName1, string _FieldName2, string _FieldName3, string _Caption1, string _Caption2, string _Caption3, string _Caption, string _format, bool _visible, int _width)
    {

        GridViewBandColumn gc_ = new GridViewBandColumn();
        gc_.HeaderStyle.HorizontalAlign = HorizontalAlign.Center;
        GridViewDataTextColumn gc_1 = new GridViewDataTextColumn();
        GridViewDataTextColumn gc_2 = new GridViewDataTextColumn();
        GridViewDataTextColumn gc_3 = new GridViewDataTextColumn();

        gc_1.FieldName = _FieldName1;
        gc_2.FieldName = _FieldName2;
        gc_3.FieldName = _FieldName3;

        gc_1.Caption = _Caption1;
        gc_2.Caption = _Caption2;
        gc_3.Caption = _Caption3;

        gc_.Columns.Add(gc_1);
        gc_.Columns.Add(gc_2);
        gc_.Columns.Add(gc_3);

        gc_.Caption = _Caption;
        gc_.Width = System.Web.UI.WebControls.Unit.Pixel(200);
        //   gc_.PropertiesTextEdit.Width = System.Web.UI.WebControls.Unit.Pixel(_width);
        gc_.ExportWidth = _width;


        gc_1.Visible = _visible;
        gc_1.Width = System.Web.UI.WebControls.Unit.Pixel(100);
        gc_1.PropertiesTextEdit.Width = System.Web.UI.WebControls.Unit.Pixel(_width);
        gc_1.ExportWidth = _width;
        gc_2.Width = System.Web.UI.WebControls.Unit.Pixel(100);
        gc_2.PropertiesTextEdit.Width = System.Web.UI.WebControls.Unit.Pixel(_width);
        gc_2.ExportWidth = _width;


        gc_3.Visible = _visible;
        gc_3.Width = System.Web.UI.WebControls.Unit.Pixel(100);
        gc_3.PropertiesTextEdit.Width = System.Web.UI.WebControls.Unit.Pixel(_width);
        gc_3.ExportWidth = _width;
        if (_format != "")
        {
            gc_1.PropertiesTextEdit.DisplayFormatString = _format;
            gc_2.PropertiesTextEdit.DisplayFormatString = _format;
            gc_3.PropertiesTextEdit.DisplayFormatString = _format;

        }
        return gc_;
    }
}