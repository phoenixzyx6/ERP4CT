<%@ Page Title="生产记录日报表" Language="C#" MasterPageFile="~/Reports/ReportsBase.Master"
    Inherits="ZLERP.Web.Reports.ReportBase" AutoEventWireup="true" %>

<script runat="server" language="c#">
    protected void Page_Load(object sender, EventArgs e)
    {
        refresh();
        if (!Page.IsPostBack)
        {
            //获取全局配置中的ChangeTime
            var Cfg_ChangeTime = SysConfigs.Where(p => p.ConfigName == "ChangeTime").FirstOrDefault();
            tbBeginDate.Text = DateTime.Today.ToString("yyyy-MM-dd") + " " + Cfg_ChangeTime.ConfigValue;
            tbEndDate.Text = DateTime.Today.AddDays(1).ToString("yyyy-MM-dd") + " " + Cfg_ChangeTime.ConfigValue;
            this.tbProductLineID.DataSource = this.SqlDataSource2;
            this.tbProductLineID.DataTextField = "ProductLineName";
            this.tbProductLineID.DataValueField = "ProductLineID";
            this.tbProductLineID.DataBind();
            this.ReportViewer1.LocalReport.DisplayName = this.Title + DateTime.Today.ToString("yyyy年MM月dd日");
            this.ReportViewer1.LocalReport.Refresh();
        }
    }
    protected void btnQuery_Click(object sender, EventArgs e)
    {
        this.ReportViewer1.LocalReport.SetParameters(new ReportParameter("BeginDate", this.tbBeginDate.Text));
        this.ReportViewer1.LocalReport.SetParameters(new ReportParameter("EndDate", this.tbEndDate.Text));
        this.ReportViewer1.LocalReport.Refresh();
    }

    public void refresh()
    {
        string CurrentYear = Session["YearAccount"] == null ? "ZLERP" : Session["YearAccount"].ToString();

        this.SqlDataSource1.ConnectionString = this.SqlDataSource1.ConnectionString.Replace("ZLERP", CurrentYear);
        this.SqlDataSource1.ProviderName = this.SqlDataSource1.ProviderName.Replace("ZLERP", CurrentYear);
        this.SqlDataSource1.DataBind();
        this.SqlDataSource2.ConnectionString = this.SqlDataSource2.ConnectionString.Replace("ZLERP", CurrentYear);
        this.SqlDataSource2.ProviderName = this.SqlDataSource2.ProviderName.Replace("ZLERP", CurrentYear);
        this.SqlDataSource2.DataBind();
    }
</script>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Header" runat="server">
    <script type="text/javascript" language="javascript">
        $(document).ready(function () {
            (function ($) {
                $.widget("ui.autocomplete", {
                    options: {
                        appendTo: "body",
                        delay: 300,
                        minLength: 1,
                        position: {
                            my: "left top",
                            at: "left bottom",
                            collision: "none"
                        },
                        source: null
                    },
                    _create: function () {
                        var self = this,
			doc = this.element[0].ownerDocument,
			suppressKeyPress;
                        this.element
			.addClass("ui-autocomplete-input")
			.attr("autocomplete", "off")
			.attr({
			    role: "textbox",
			    "aria-autocomplete": "list",
			    "aria-haspopup": "true"
			})
			.bind("keydown.autocomplete", function (event) {
			    if (self.options.disabled || self.element.attr("readonly")) {
			        return;
			    }

			    suppressKeyPress = false;
			    var keyCode = $.ui.keyCode;
			    switch (event.keyCode) {
			        case keyCode.PAGE_UP:
			            self._move("previousPage", event);
			            break;
			        case keyCode.PAGE_DOWN:
			            self._move("nextPage", event);
			            break;
			        case keyCode.UP:
			            self._move("previous", event);
			            event.preventDefault();
			            break;
			        case keyCode.DOWN:
			            self._move("next", event);
			            event.preventDefault();
			            break;
			        case keyCode.ENTER:
			        case keyCode.NUMPAD_ENTER:
			            if (self.menu.active) {

			                suppressKeyPress = true;
			                event.preventDefault();
			            }
			        case keyCode.TAB:
			            if (!self.menu.active) {
			                return;
			            }
			            self.menu.select(event);
			            break;
			        case keyCode.ESCAPE:
			            self.element.val(self.term);
			            self.close(event);
			            break;
			        default:
			            clearTimeout(self.searching);
			            self.searching = setTimeout(function () {
			                if (self.term != self.element.val()) {
			                    self.selectedItem = null;
			                    self.search(null, event);
			                }
			            }, self.options.delay);
			            break;
			    }
			})
			.bind("keypress.autocomplete", function (event) {
			    if (suppressKeyPress) {
			        suppressKeyPress = false;
			        event.preventDefault();
			    }
			})
			.bind("focus.autocomplete", function () {
			    if (self.options.disabled) {
			        return;
			    }
			    self.selectedItem = null;
			    self.previous = self.element.val();
			})
			.bind("blur.autocomplete", function (event) {
			    if (self.options.disabled) {
			        return;
			    }
			    clearTimeout(self.searching);
			    self.closing = setTimeout(function () {
			        self.close(event);
			        self._change(event);
			    }, 150);
			})
            .bind('input', function (c) {//jQuery UI Autocomplete 1.8.*中文输入修正
                self.search(self.item);
            });
                        this._initSource();
                        this.response = function () {
                            return self._response.apply(self, arguments);
                        };
                        this.menu = $("<ul></ul>")
			.addClass("ui-autocomplete")
			.appendTo($(this.options.appendTo || "body", doc)[0])
			.mousedown(function (event) {
			    var menuElement = self.menu.element[0];
			    if (!$(event.target).closest(".ui-menu-item").length) {
			        setTimeout(function () {
			            $(document).one('mousedown', function (event) {
			                if (event.target !== self.element[0] &&
								event.target !== menuElement &&
								!$.ui.contains(menuElement, event.target)) {
			                    self.close();
			                }
			            });
			        }, 1);
			    }
			    setTimeout(function () {
			        clearTimeout(self.closing);
			    }, 13);
			})
			.menu({
			    focus: function (event, ui) {
			        var item = ui.item.data("item.autocomplete");
			        if (false !== self._trigger("focus", event, { item: item })) {
			            if (/^key/.test(event.originalEvent.type)) {
			                self.element.val(item.value);
			            }
			        }
			    },
			    selected: function (event, ui) {
			        var item = ui.item.data("item.autocomplete"),
						previous = self.previous;
			        if (self.element[0] !== doc.activeElement) {
			            self.element.focus();
			            self.previous = previous;
			            setTimeout(function () {
			                self.previous = previous;
			                self.selectedItem = item;
			            }, 1);
			        }

			        if (false !== self._trigger("select", event, { item: item })) {
			            self.element.val(item.value);
			        }
			        self.term = self.element.val();

			        self.close(event);
			        self.selectedItem = item;
			    },
			    blur: function (event, ui) {
			        if (self.menu.element.is(":visible") &&
						(self.element.val() !== self.term)) {
			            self.element.val(self.term);
			        }
			    }
			})
			.zIndex(this.element.zIndex() + 1)
			.css({ top: 0, left: 0 })
			.hide()
			.data("menu");
                        if ($.fn.bgiframe) {
                            this.menu.element.bgiframe();
                        }
                    },

                    destroy: function () {
                        this.element
			.removeClass("ui-autocomplete-input")
			.removeAttr("autocomplete")
			.removeAttr("role")
			.removeAttr("aria-autocomplete")
			.removeAttr("aria-haspopup");
                        this.menu.element.remove();
                        $.Widget.prototype.destroy.call(this);
                    },

                    _setOption: function (key, value) {
                        $.Widget.prototype._setOption.apply(this, arguments);
                        if (key === "source") {
                            this._initSource();
                        }
                        if (key === "appendTo") {
                            this.menu.element.appendTo($(value || "body", this.element[0].ownerDocument)[0])
                        }
                    },

                    _initSource: function () {
                        var self = this,
			array,
			url;
                        if ($.isArray(this.options.source)) {
                            array = this.options.source;
                            this.source = function (request, response) {
                                response($.ui.autocomplete.filter(array, request.term));
                            };
                        } else if (typeof this.options.source === "string") {
                            url = this.options.source;
                            this.source = function (request, response) {
                                if (self.xhr) {
                                    self.xhr.abort();
                                }
                                self.xhr = $.ajax({
                                    url: url,
                                    data: request,
                                    dataType: "json",
                                    success: function (data, status, xhr) {
                                        if (xhr === self.xhr) {
                                            response(data);
                                        }
                                        self.xhr = null;
                                    },
                                    error: function (xhr) {
                                        if (xhr === self.xhr) {
                                            response([]);
                                        }
                                        self.xhr = null;
                                    }
                                });
                            };
                        } else {
                            this.source = this.options.source;
                        }
                    },

                    search: function (value, event) {
                        value = value != null ? value : this.element.val();

                        this.term = this.element.val();

                        if (value.length < this.options.minLength) {
                            return this.close(event);
                        }

                        clearTimeout(this.closing);
                        if (this._trigger("search", event) === false) {
                            return;
                        }

                        return this._search(value);
                    },

                    _search: function (value) {
                        this.element.addClass("ui-autocomplete-loading");

                        this.source({ term: value }, this.response);
                    },

                    _response: function (content) {
                        if (content && content.length) {
                            content = this._normalize(content);
                            this._suggest(content);
                            this._trigger("open");
                        } else {
                            this.close();
                        }
                        this.element.removeClass("ui-autocomplete-loading");
                    },

                    close: function (event) {
                        clearTimeout(this.closing);
                        if (this.menu.element.is(":visible")) {
                            this.menu.element.hide();
                            this.menu.deactivate();
                            this._trigger("close", event);
                        }
                    },

                    _change: function (event) {
                        if (this.previous !== this.element.val()) {
                            this._trigger("change", event, { item: this.selectedItem });
                        }
                    },

                    _normalize: function (items) {
                        if (items.length && items[0].label && items[0].value) {
                            return items;
                        }
                        return $.map(items, function (item) {
                            if (typeof item === "string") {
                                return {
                                    label: item,
                                    value: item
                                };
                            }
                            return $.extend({
                                label: item.label || item.value,
                                value: item.value || item.label
                            }, item);
                        });
                    },
                    _suggest: function (items) {
                        var ul = this.menu.element
			.empty()
			.zIndex(this.element.zIndex() + 1);
                        this._renderMenu(ul, items);
                        this.menu.deactivate();
                        this.menu.refresh();
                        ul.show();
                        this._resizeMenu();
                        ul.position($.extend({
                            of: this.element
                        }, this.options.position));
                    },

                    _resizeMenu: function () {
                        var ul = this.menu.element;
                        ul.outerWidth(Math.max(
			ul.width("").outerWidth(),
			this.element.outerWidth()
		));
                    },
                    _renderMenu: function (ul, items) {
                        var self = this;
                        $.each(items, function (index, item) {
                            self._renderItem(ul, item);
                        });
                    },
                    _renderItem: function (ul, item) {
                        return $("<li></li>")
			.data("item.autocomplete", item)
			.append($("<a></a>").text(item.label))
			.appendTo(ul);
                    },
                    _move: function (direction, event) {
                        if (!this.menu.element.is(":visible")) {
                            this.search(null, event);
                            return;
                        }
                        if (this.menu.first() && /^previous/.test(direction) ||
				this.menu.last() && /^next/.test(direction)) {
                            this.element.val(this.term);
                            this.menu.deactivate();
                            return;
                        }
                        this.menu[direction](event);
                    },

                    widget: function () {
                        return this.menu.element;
                    }
                });
                $.extend($.ui.autocomplete, {
                    escapeRegex: function (value) {
                        return value.replace(/[-[\]{}()*+?.,\\^$|#\s]/g, "\\$&");
                    },
                    filter: function (array, term) {
                        var matcher = new RegExp($.ui.autocomplete.escapeRegex(term), "i");
                        return $.grep(array, function (value) {
                            return matcher.test(value.label || value.value || value);
                        });
                    }
                });

            } (jQuery));
            //        $('#<%=tbBeginDate.ClientID %>').datetimepicker();
            //        $('#<%=tbEndDate.ClientID %>').datetimepicker();
            var dates = $("#<%=tbBeginDate.ClientID %>, #<%=tbEndDate.ClientID %>").datetimepicker({
                defaultDate: "+1w",
                changeMonth: true,
                numberOfMonths: 2,
                onSelect: function (selectedDate) {
                    var option = this.id == "<%=tbBeginDate.ClientID %>" ? "minDate" : "maxDate",
					instance = $(this).data("datepicker"),
					date = $.datepicker.parseDate(
						instance.settings.dateFormat ||
						$.datepicker._defaults.dateFormat,
						selectedDate, instance.settings);
                    dates.not(this).datepicker("option", option, date);
                }
            });
            $("#<%=tbProjectName.ClientID%>").autocomplete({
                delay: 500,
                minLength: 2,
                source: function (request, response) {
                    $.ajax({
                        url: "/Project.mvc/GetProjectByName",
                        dataType: "json",
                        type: "POST",
                        data: {
                            ProjectName: request.term
                        },
                        success: function (data) {
                            response($.map(data.Name, function (item) {
                                return {
                                    label: item.ProjectName,
                                    value: item.ProjectName
                                }
                            }));
                        },
                        error: function (XMLHttpRequest, textStatus, errorThrown) {
                            return false;
                        }
                    });
                },
                open: function () {
                    $(this).removeClass("ui-corner-all").addClass("ui-corner-top");
                },
                close: function () {
                    $(this).removeClass("ui-corner-top").addClass("ui-corner-all");
                }
            });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Body" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    起始时间：<asp:TextBox ID="tbBeginDate" runat="server"></asp:TextBox>
    截止时间：<asp:TextBox ID="tbEndDate" runat="server"></asp:TextBox>
    生产线：<asp:DropDownList ID="tbProductLineID" runat="server" Width="150px">
    </asp:DropDownList>
    <div style="display:none">工程名称：<asp:TextBox ID="tbProjectName" runat="server" Width="180px"></asp:TextBox></div>

    <!--新加了查询条件-->
    工程名称：<asp:TextBox ID="tbProjectName1" Width="100" runat="server"></asp:TextBox>
    砼强度：<asp:TextBox ID="tbConStrength" Width="100" runat="server"></asp:TextBox>

    <asp:Button ID="btnQuery" OnClick="btnQuery_Click" runat="server" Text="查询" />
    <rsweb:ReportViewer ID="ReportViewer1" Width="100%" Height="500px" runat="server"
        Font-Names="Verdana" Font-Size="9pt" InteractiveDeviceInfos="(集合)" WaitMessageFont-Names="Verdana"
        WaitMessageFont-Size="12pt" SizeToReportContent="True">
        <LocalReport ReportPath="Reports\Produce\R410801.rdlc" DisplayName="生产记录日报表">
            <DataSources>
                <rsweb:ReportDataSource DataSourceId="SqlDataSource1" Name="DataSet1" />
            </DataSources>
        </LocalReport>
    </rsweb:ReportViewer>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ZLERP %>"
        ProviderName="<%$ ConnectionStrings:ZLERP.ProviderName %>" SelectCommand="sp_rpt_productrecord_daily"
        SelectCommandType="StoredProcedure" CancelSelectOnNullParameter="False">
        <SelectParameters>
            <asp:ControlParameter ControlID="tbBeginDate" Name="StartDateTime" PropertyName="Text" />
            <asp:ControlParameter ControlID="tbEndDate" Name="EndDateTime" PropertyName="Text" />
            <asp:ControlParameter ControlID="tbProductLineID" Name="ProductLineID" PropertyName="Text" />
            <asp:ControlParameter ControlID="tbProjectName" Name="ProjectName" PropertyName="Text" />

            <asp:ControlParameter ControlID="tbConStrength" Name="conStrength" PropertyName="Text" />     
            <asp:ControlParameter ControlID="tbProjectName1" Name="projectname1" PropertyName="Text" />

        </SelectParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:ZLERP %>"
        ProviderName="<%$ ConnectionStrings:ZLERP.ProviderName %>" SelectCommand="select ''  ProductLineID, '' ProductLineName  union  select ProductLineID,ProductLineName from ProductLine  where IsUsed=1"
        SelectCommandType="Text" CancelSelectOnNullParameter="False"></asp:SqlDataSource>
</asp:Content>
