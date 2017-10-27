<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SC1ReportLink.aspx.cs" Inherits="SMPL46.Reports.SC1ReportLink" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Ealing PMS Street Cleaning</title>
    <link href="../Content/Styles.css" type="text/css" rel="stylesheet"/>
</head>
<body>
    <form id="Default" method="post" runat="server">
    
        <div class="body-topbar">
            <div class="navbar-header" style="float: left;">
                <img src="../images/logosplashh.png" alt="" height="50" style="margin-left: 50px; margin-top: 10px; margin-bottom: 10px;" />
            </div>
        </div>

        <div style="clear: both; padding-top: 20px;">
            <div style="margin-left: 25%; width: 50%;">
                <asp:Panel ID="PanDisplayNotice" Visible="false" runat="server">
                    <asp:GridView ID="gvDisplayNotice" runat="server" Visible="False" CssClass="MasterGrid"
                                    GridLines="Vertical" DataKeyNames="Cleaning_Schedule_PKUID" AutoGenerateColumns="False"
                                    Width="100%" CellPadding="4" ForeColor="Black" BackColor="White" BorderColor="#DEDFDE"
                                    BorderStyle="None" BorderWidth="1px">
                        <Columns>
                            <asp:BoundField HeaderText="Notice Type" DataField="Notice_type_description" ReadOnly="True">
                                <ItemStyle Width="15%" Wrap="True" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Notice Reference" DataField="Notice_Reference" ReadOnly="True">
                                <ItemStyle Width="15%" Wrap="True" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Notice Served" DataField="Notice_served" ReadOnly="True">
                                <ItemStyle Width="15%" Wrap="True" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Rectification Submitted" DataField="Rectification_submitted" ReadOnly="True">
                                <ItemStyle Width="15%" Wrap="True" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Notice Type Code" DataField="Notice_type_code" Visible="false" ReadOnly="True">
                                <ItemStyle Width="15%" Wrap="True" />
                            </asp:BoundField>                                                            
                            <asp:BoundField HeaderText="Due for Rectification" DataField="Rectification_Due"
                                            ReadOnly="True">
                                <ItemStyle Width="15%" Wrap="True" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Notice_Frequency_Link_ID" DataField="Notice_Frequency_Link_ID"
                                            ReadOnly="True" Visible="false">
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Parent_PKUID" DataField="Parent_PKUID"
                                            ReadOnly="True" Visible="false">
                            </asp:BoundField>                                                            
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <h2>
                                        <a class="linkButton"  href="PrintNotice.aspx?pkuid=<%#
                                                    Eval("Cleaning_Schedule_PKUID")%>&notice_type=<%#
                                                    Eval("notice_type_code")%>&notice_frequency_link_id=<%#
                                                    Eval("Notice_Frequency_Link_ID")%>&parent_pkuid=<%#
                                                    Eval("Parent_PKUID")%>" target="_blank">
                                            Show Notice</a></h2>
                                </ItemTemplate>
                                <ItemStyle Width="25%" HorizontalAlign="Center" />
                            </asp:TemplateField>
                        </Columns>
                        <RowStyle BackColor="#F7F7DE" />
                        <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
                        <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#F2D86D" Font-Bold="True" ForeColor="White" />
                        <AlternatingRowStyle BackColor="White" />
                    </asp:GridView>
                </asp:Panel>
            </div>
            <div style="margin-left: 25%; width: 50%; padding-top: 20px;">
                <asp:Panel ID="panLoggedOn" runat="server">
                    <asp:Panel ID="panInspList" runat="server">
                        <br/>
                        <asp:DataGrid ID="dgrInspList" runat="server" CssClass="normal" CellPadding="2" AutoGenerateColumns="False"
                                        ForeColor="Blue">
                            <HeaderStyle Font-Bold="True"></HeaderStyle>
                            <Columns>
                                <asp:BoundColumn DataField="ID" SortExpression="ID" HeaderText="NR">
                                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="ward" SortExpression="ward" HeaderText="Ward">
                                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="Street_Name" SortExpression="Street_Name" HeaderText="Street">
                                    <HeaderStyle HorizontalAlign="Center" Width="50px"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="Limits" SortExpression="Limits" HeaderText="Limits">
                                    <HeaderStyle HorizontalAlign="Center" Width="50px"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="Std_or_deep" SortExpression="Std_or_deep" HeaderText="Std or Deep">
                                    <HeaderStyle HorizontalAlign="Center" Width="50px"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="Sched_Cleaning_Date" SortExpression="Sched_Cleaning_Date"
                                                    HeaderText="Sched. Cleaning" DataFormatString="{0:dd-MMM-yyyy}">
                                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:BoundColumn>
                                <asp:HyperLinkColumn DataNavigateUrlField="pkUID" DataNavigateUrlFormatString="sc1Report.aspx?id={0}"
                                                        DataTextField="Inspected" HeaderText="Inspected">
                                    <HeaderStyle HorizontalAlign="Center" Width="50px"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:HyperLinkColumn>
                            </Columns>
                        </asp:DataGrid>
                        <br/>
                    </asp:Panel>
                    <asp:Panel ID="panInspDetails" runat="server">
                        <br/>
                        <asp:Table class="normal" runat="server" BackColor="White" BorderColor="Black" BorderWidth="1" ForeColor="Black" GridLines="Both" BorderStyle="Solid">
                            <asp:TableRow>
                                <asp:TableCell style="width: auto">
                                    <b>Ward</b></asp:TableCell>
                                <asp:TableCell>
                                    <asp:Label ID="lblWard" runat="server"></asp:Label></asp:TableCell>
                            </asp:TableRow>
                            <asp:TableRow>
                                <asp:TableCell style="width: auto">
                                    <b>District</b></asp:TableCell>
                                <asp:TableCell>
                                    <asp:Label ID="lblDistrict" runat="server"></asp:Label></asp:TableCell>
                            </asp:TableRow>
                            <asp:TableRow>
                                <asp:TableCell style="width: auto">
                                    <b>Street</b></asp:TableCell>
                                <asp:TableCell>
                                    <asp:Label ID="lblStreet" runat="server"></asp:Label></asp:TableCell>
                            </asp:TableRow>
                            <asp:TableRow>
                                <asp:TableCell style="width: auto">
                                    <b>Limits</b></asp:TableCell>
                                <asp:TableCell>
                                    <asp:Label ID="lblLimits" runat="server"></asp:Label></asp:TableCell>
                            </asp:TableRow>
                            <asp:TableRow>
                                <asp:TableCell style="width: auto">
                                    <b>Std or Deep</b></asp:TableCell>
                                <asp:TableCell>
                                    <asp:Label ID="lblStdOrDeep" runat="server"></asp:Label></asp:TableCell>
                            </asp:TableRow>
                            <asp:TableRow>
                                <asp:TableCell style="width: auto">
                                    <b>Frequency of Cleaning</b></asp:TableCell>
                                <asp:TableCell>
                                    <asp:Label ID="lblFreqCode" runat="server"></asp:Label></asp:TableCell>
                            </asp:TableRow>  
                            <asp:TableRow>
                                <asp:TableCell style="width: auto">
                                    <b>Scheduled Cleaning Date</b></asp:TableCell>
                                <asp:TableCell>
                                    <asp:Label ID="lblSchedCleaningDate" runat="server"></asp:Label></asp:TableCell>
                            </asp:TableRow>
                            <asp:TableRow>
                                <asp:TableCell style="width: 246px">
                                    <b>Initial Date and Time Inspected</b></asp:TableCell>
                                <asp:TableCell>
                                    <asp:Label ID="lblInspectedDate" runat="server"></asp:Label></asp:TableCell>
                            </asp:TableRow>
                            <asp:TableRow>
                                <asp:TableCell style="width: auto">
                                    <b>Initial Inspector</b></asp:TableCell>
                                <asp:TableCell>
                                    <asp:Label ID="lblInspector" runat="server"></asp:Label></asp:TableCell>
                            </asp:TableRow>
                            <asp:TableRow>
                                <asp:TableCell style="width: auto">
                                    <b>Result of Initial Inspection</b><br>
                                                                        (Grade of Cleanliness)</asp:TableCell>
                                <asp:TableCell>
                                    <asp:Label ID="lblInspectionGrade" runat="server"></asp:Label></asp:TableCell>
                            </asp:TableRow>
                            <asp:TableRow>
                                <asp:TableCell style="height: 21px; width: auto;">
                                    <b>Initial Reason</b></asp:TableCell>
                                <asp:TableCell>
                                    <asp:Label ID="lblReason" runat="server"></asp:Label></asp:TableCell>
                            </asp:TableRow>
                            <asp:TableRow>
                                <asp:TableCell style="height: 22px; width: auto;">
                                    <b>Initial Inspection Comments</b></asp:TableCell>
                                <asp:TableCell style="height: 22px">
                                    <asp:Label ID="lblInspectionComments" runat="server"></asp:Label></asp:TableCell>
                            </asp:TableRow>
                            <asp:TableRow>
                                <asp:TableCell style="height: 55px" colspan="2">
                                    <b>Photo of the Street after Initial Inspection</b><br>
                                                                                        <asp:Image ID="imgInspPhoto" runat="server" style="width: 600px"></asp:Image>
                                                                                        <asp:Label ID="lblNoInspPhoto" runat="server">No photo available.</asp:Label></asp:TableCell>
                            </asp:TableRow>
                            <asp:TableRow>
                                <asp:TableCell style="height: 22px; width: auto;">
                                    <b>ReInspection Date and Time</b></asp:TableCell>
                                <asp:TableCell>
                                    <asp:Label ID="lblReInspectionDate" runat="server"></asp:Label></asp:TableCell>
                            </asp:TableRow>
                            <asp:TableRow>
                                <asp:TableCell style="width: auto">
                                    <b>ReInspection Inspector</b></asp:TableCell>
                                <asp:TableCell>
                                    <asp:Label ID="lblReInspector" runat="server"></asp:Label></asp:TableCell>
                            </asp:TableRow>
                            <asp:TableRow>
                                <asp:TableCell style="width: auto">
                                    <b>Result of ReInspection</b><br>
                                                                    (Grade of Cleanliness)</asp:TableCell>
                                <asp:TableCell>
                                    <asp:Label ID="lblReInspectionGrade" runat="server"></asp:Label></asp:TableCell>
                            </asp:TableRow>
                            <asp:TableRow>
                                <asp:TableCell style="height: 22px; width: auto;">
                                    <b>ReInspection Reason</b></asp:TableCell>
                                <asp:TableCell>
                                    <asp:Label ID="lblReInspectionReason" runat="server"></asp:Label></asp:TableCell>
                            </asp:TableRow>
                            <asp:TableRow>
                                <asp:TableCell style="height: 22px; width: auto;">
                                    <b>ReInspection Comments</b></asp:TableCell>
                                <asp:TableCell style="height: 22px">
                                    <asp:Label ID="lblReInspectionComments" runat="server"></asp:Label></asp:TableCell>
                            </asp:TableRow>
                            <asp:TableRow>
                                <asp:TableCell style="height: 55px" colspan="2">
                                    <b>Photo of the Street after ReInspection</b><br>
                                                                                    <asp:Image ID="imgReInspPhoto" runat="server" style="width: 600px"></asp:Image>
                                                                                    <asp:Label ID="lblNoReInspPhoto" runat="server">No photo available.</asp:Label></asp:TableCell>
                            </asp:TableRow>
                        </asp:Table>
                    </asp:Panel>
                </asp:Panel>
            </div>
        </div>

        <footer style="height: 100px; background-color: white;">
            <div style="float: left; width: auto">
                <img src="../images/iBase_logoTransparent.png" alt="" height="100" style="margin-left: 50px; margin-top: 0px; margin-bottom: 10px;" />
            </div>
            <div style="float: left; color: black; margin-left: 50px; margin-top: 50px; font-size: 15pt; font-weight: bold;">
                <p>&copy; 2016 - Ealing SMPL #2</p>
            </div>
            <div style="float: right; width: auto">
                <img src="../images/Ealing_Logo_Colour_CMYK.jpg" alt="" height="100" style="margin-left: 50px; margin-top: 0px; margin-bottom: 10px;"/>
            </div>
            <div style="clear: both;"></div>
        </footer>
    </form>
</body>
</html>
