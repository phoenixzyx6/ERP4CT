<%@ Page Language="C#" ContentType="text/html" ResponseEncoding="utf-8" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>预拌砼买卖合同</title>
    <style type="text/css">
        @media print
        {
            input, select, textarea
            {
                border: none;
            }
            div.PageBlank
            {
                display: none;
            }
            .page
            {
                border: 3px #F00 solid;
            }
        }
        body
        {
            padding: 0px;
            margin: 0 0 30px 0;
            border: none;
            text-align: center;
            font-size: 14px;
        }
        .HeaderText
        {
            font-family: "方正舒体";
            font-size: 20px;
            color: #CCC;
            text-align: left;
        }
        .FooterText
        {
            font-family: "方正舒体";
            font-size: 20px;
            color: #CCC;
            text-align: center;
            padding-bottom: 10px;
        }
        .contract
        {
            margin: 0 auto;
            width: 670px;
            background-color: #fff;
            font-size: 14px;
            text-align: center;
        }
        .page
        {
            border: 1px solid #fff;
        }
        .tablehover
        {
            border: #000 solid 1px; /*background-color: #FC9;*/
        }
        .table
        {
            border: #000 1px solid;
            border-collapse: collapse;
        }
        .table tr td
        {
            border: #000 1px solid;
        }
        #contractBorder
        {
            border: 1px solid #000;
        }
        .PageNext
        {
            page-break-after: always;
        }
    </style>
    <script src="<%=ResolveUrl("~/Scripts/jquery-1.4.4.min.js")%>" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $("div.page").hover(function () {
                $(this).addClass("tablehover");

            }, function () {
                $(this).removeClass("tablehover");

            });
            //获取页面参数
            var customerId = '<%=Request.QueryString["customerid"] %>';
            var contractId = '<%=Request.QueryString["contractid"] %>';
            var projectId = '<%=Request.QueryString["projectid"] %>';
            //取任务单列表
            $.post(
                "/ProduceTask.mvc/Find",
                {
                    condition: "ContractID='" + contractId + "' and ProjectID='" + projectId + "'",
                    page: 1,
                    rows: 30,
                    sidx: "ID",
                    sord: "asc"
                },
                function (resp) {
                    var data = resp.rows;
                    //取供方、需方、工程、地址
                    if (data.length > 0) var commondata = data[0];
                    var customername = commondata.ConstructUnit; //需方
                    var projectname = commondata.ProjectName; //工程
                    var projectaddr = commondata.ProjectAddr; //工程
                    $("input[name='ContractID']").val(contractId);
                    $("input[name='ProjectName']").val(projectname);
                    $("input[name='CustomerName']").val(customername);
                    $("input[name='SupplyUnitName']").val();
                    $("input[name='ProjectAddr']").val(projectaddr);
                    var all_tr = "";
                    var tr_str_begin = "<tr>";
                    var tr_str_end = "</tr>";
                    var td_str = "";
                    var first_td_str = "";
                    $.each(data, function (i, n) {
                        td_str += "<td width='14%' height='30' valign='middle'>" + n.ConsPos + "</td>";
                        td_str += "<td width='7%' valign='middle'>" + n.ConStrength + "</td>";
                        td_str += "<td width='14%' valign='middle'>" + n.Slump + "</td>";
                        td_str += "<td width='11%' valign='middle'>" + n.PlanCube + "</td>";
                        td_str += "<td width='12%' valign='middle'><input type='text' id='price" + i + "' name='price' style='width:60px; text-align:center;' /></td>";
                        td_str += "<td width='14%' valign='middle'>" + n.CastMode + "</td>";
                        if (i == 0) { //第0行有rowspan
                            first_td_str += "<td width='8%' id='rowspancol' rowspan='" + data.length + "' align='center' valign='middle'>预拌<br />混凝<br />土</td>" + td_str + "<td width='16%' rowspan='" + data.length + "' valign='top'><textarea id='specYQ' name='specYQ' rows='4' cols='8' style='width:auto; height:" + (data.length == 1 ? (30 * data.length + 10) : (30 * data.length)) + "px;'></textarea></td>";
                            all_tr += tr_str_begin + first_td_str + tr_str_end;
                        } else {
                            all_tr += tr_str_begin + td_str + tr_str_end;
                        }
                        td_str = "";
                    });
                    $("table#cement_tb").find("tr").next().before(all_tr);
                }
            );
            //取客户信息
            $.post(
                "/Customer.mvc/Get",
                { id: customerId },
                function (resp) {
                    if (resp.Data) {
                        $("#BLegal").val(resp.Data.Principal);
                        $("#BSigner").val(resp.Data.LinkMan);
                        $("#BManager").val(resp.Data.LinkMan);
                        $("#BlinkAddr").val(resp.Data.BusinessAddr);
                    }
                }
            );
            //取银行信息
            $.post(
                "/Bank.mvc/FindBank",
                {
                    CustomerID: customerId
                },
                function (resp) {
                    //根据IsGuarantee来判断填充担保还是非担保
                    var banks = resp.Data;
                    if (banks) {
                        $.each(banks, function (i, n) {
                            if (!n.IsGuarantee) {
                                $("#BBankName").val(n.AccountName);
                                $("#BBank").val(n.BankName);
                                $("#BBankAccount").val(n.Account);
                            } else {
                                $("#DBBankName").val(n.AccountName);
                                $("#DBBank").val(n.BankName);
                                $("#DBBankAccount").val(n.Account);
                            }
                        });
                    }
                }
            );
        });
    </script>
</head>
<body>
    <div class="contract">
        <!--封面-->
        <div id="frontcover" class="page">
            <div class="contractid" style="text-align: right; height: 30px; padding-right: 15px;
                line-height: 30px;">
                合同编号：
                <input type="text" id="ContractID" name="ContractID" value="" style="width: 100px;" />
            </div>
            <div class="contracttitle" style="height: 80px; line-height: 80px; font-size: 28px;
                font-weight: bold; font-family: '宋体'; padding-bottom: 350px;">
                郴州市预拌砼买卖合同</div>
            <div style="vertical-align: middle; height: 60px; line-height: 60px; font-size: 20px;
                font-weight: bold;">
                工&nbsp;程&nbsp;名&nbsp;称：
                <input type="text" id="ProjectName" name="ProjectName" style="width: 350px; text-decoration: none;
                    height: 25px; line-height: 25px; border: none; border-bottom: 1px solid #000;
                    font-size: 20px; font-weight: bold; padding-left: 12px; letter-spacing: 1px;"
                    value="" />
            </div>
            <div style="vertical-align: middle; height: 60px; line-height: 60px; font-size: 20px;
                font-weight: bold;">
                需&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;方：
                <input type="text" id="CustomerName" name="CustomerName" style="width: 350px; text-decoration: none;
                    height: 25px; line-height: 25px; border: none; border-bottom: 1px solid #000;
                    font-size: 20px; font-weight: bold; padding-left: 12px; letter-spacing: 1px;"
                    value="" />
            </div>
            <div style="vertical-align: middle; height: 60px; line-height: 60px; font-size: 20px;
                font-weight: bold;">
                供&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;方：
                <input type="text" id="SupplyUnitName" name="SupplyUnitName" style="width: 350px;
                    text-decoration: none; height: 25px; line-height: 25px; border: none; border-bottom: 1px solid #000;
                    font-size: 20px; font-weight: bold; padding-left: 12px; letter-spacing: 1px;"
                    value="郴州泰湘混凝土有限公司" />
            </div>
            <div style="height: 30px; line-height: 30px; vertical-align: bottom; padding-top: 200px;
                padding-bottom: 70px; font-size: 20px;">
                郴州市混凝土协会监制</div>
        </div>
        <div class="PageNext">
            <!--下一页-->
        </div>
        <div class="PageBlank" style="height: 15px;">
            &nbsp;</div>
        <div id="page1" class="page">
            <div style="font-size: 24px; font-weight: bold; height: 30px; line-height: 30px;
                padding-top: 3px;">
                郴州市预拌混凝土买卖合同</div>
            <div style="text-align: right; height: 30px; padding-right: 15px; line-height: 30px;">
                合同编号：
                <input type="text" id="ContractID2" name="ContractID" style="width: 100px;" />
            </div>
            <div style="text-align: left; font-weight: bold; padding-left: 20px;">
                <p style="letter-spacing: 2px; font-size: 16px;">
                    需&nbsp;&nbsp;&nbsp;方：
                    <input type="text" id="CustomerName2" name="CustomerName" style="width: 250px; text-decoration: none;
                        height: 20px; line-height: 20px; border: none; border-bottom: 1px solid #000;
                        font-size: 16px; font-weight: bold; padding-left: 12px; letter-spacing: 1px;" /></p>
                <p style="letter-spacing: 2px; font-size: 16px;">
                    供&nbsp;&nbsp;&nbsp;方：
                    <input type="text" id="SupplyUnitName2" name="SupplyUnitName" style="width: 250px;
                        text-decoration: none; height: 20px; line-height: 20px; border: none; border-bottom: 1px solid #000;
                        font-size: 16px; font-weight: bold; padding-left: 12px; letter-spacing: 1px;"
                        value="郴州泰湘混凝土有限公司" /></p>
                <p style="letter-spacing: 1px; font-size: 16px;">
                    工程名称：
                    <input type="text" id="ProjectName2" name="ProjectName" style="width: 250px; text-decoration: none;
                        height: 20px; line-height: 20px; border: none; border-bottom: 1px solid #000;
                        font-size: 16px; font-weight: bold; padding-left: 12px; letter-spacing: 1px;" /></p>
                <p style="letter-spacing: 1px; font-size: 16px;">
                    工程地点：
                    <input type="text" id="ProjectAddr2" name="ProjectAddr" style="width: 250px; text-decoration: none;
                        height: 20px; line-height: 20px; border: none; border-bottom: 1px solid #000;
                        font-size: 16px; font-weight: bold; padding-left: 12px; letter-spacing: 1px;" /></p>
            </div>
            <div style="text-align: left; padding-left: 20px; line-height: 25px;">
                &nbsp;&nbsp;&nbsp;&nbsp;为保护双方的合法权益，明确双方责任。依照《中华人民共和国合同法》、国家规范《预搅拌混凝土》(GB/T14902-2003)及《湖南省预拌混凝土质量管理细则》（试行）相关法律、法规。遵循公平和诚信的原则，经供需双方协商一致，签订本合同。</div>
            <div style="text-align: left; font-weight: bold; font-size: 16px; padding-left: 20px;
                line-height: 25px; padding-top: 10px;">
                第一条 预拌混凝土的品种、浇筑部位、强度等级、坍落度、数量、单价等如下表：</div>
            <div>
                <table id="cement_tb" class="table" border="0" cellspacing="0" cellpadding="0" width="100%">
                    <tr>
                        <td width="8%">
                            产品
                            <br />
                            名称
                        </td>
                        <td width="14%" valign="top">
                            浇 筑
                            <br />
                            部 位
                        </td>
                        <td width="7%" valign="top">
                            强度
                            <br />
                            级别
                        </td>
                        <td width="14%" valign="top">
                            坍落度
                            <br />
                            (mm)
                        </td>
                        <td width="11%" valign="top">
                            数量
                            <br />
                            (m3)
                        </td>
                        <td width="12%" valign="top">
                            单 价
                            <br />
                            （元/m3）
                        </td>
                        <td width="14%" align="center" valign="middle">
                            工 地<br />
                            输送方式
                        </td>
                        <td width="16%" align="center" valign="middle">
                            备 注
                            <br />
                            （特种要求）
                        </td>
                    </tr>
                    <tr>
                        <td width="100%" colspan="8" align="left" valign="top" style="font-size: 12px;">
                            <p style="padding-left: 20px; line-height: 20px;">
                                说明：<br>
                                1、混凝土价格具体结算数量和标号以实际供货数量和标号为准；
                                <br />
                                2、以上单价为砼到市区工地价含运输费；
                                <br />
                                3、混凝土单价以C30价格为基础，每增加一个等级增加人民币12元/m3，每递减一个等级减少人民币12元/m3；
                                <br />
                                4、润管砂浆400元/m3，另补空载费200元/车；
                                <br />
                                5、抗渗等级等于S6（P6）的防水混凝土价格在同品种、标号价格基础上增加20元/m3；
                                <br />
                                6、早强混凝土、水下桩混凝土价格在同品种、标号价格基础上增加15元/m3；
                                <br />
                                7、细石混凝土、加膨胀剂的抗渗混凝土价格在同品种、标号价格基础上增加50元/m3；
                                <br />
                                8、抗折4.5混凝土在同品种价格基础上增加20元/m3，抗折5.0在同品种增加30元/m3；
                                <br />
                                9、C45（含C45）以上标号混凝土价格在等级差价格基础上另加30元/m3;<br />
                                10、合同期内如混凝土主要原材料市场单价浮动超过±3％时（含3％），双方及时根据市场材料调价，调价方书面通知对方，调价通知书为本合同的补充和有效组成部分。</p>
                        </td>
                    </tr>
                </table>
            </div>
            <div style="text-align: left; font-weight: bold; font-size: 16px; padding-left: 20px;
                line-height: 25px; padding-top: 10px;">
                第二条 货款结算及支付</div>
            <div style="text-align: left; padding-left: 30px; line-height: 25px;">
                1、结算方式：每供应完一批次预拌混凝土，供方凭送货凭证编制结算单给需方，需方应在收到结算单的2天内核对数量及金额并确认，如因需方原因造成无法确对确认，供方则以需方签收的混凝土送货单为结算依据。<br />
            </div>
            <div style="text-align: left; padding-left: 30px; line-height: 25px;">
                2、付款方式:<br />
                <textarea id="Payment" name="Payment" cols="75" rows="1" wrap="virtual" style="line-height: 25px;
                    height: 50px; text-decoration: underline;"></textarea>
                <br />
                3、工地停工或需方其他原因停工造成工地停供预拌混凝土时，需方应自停工之日起7天内付清所欠供方全部混凝土货款。<br />
                4、余款结算：混凝土浇筑完成款项在30天内一次性付清。
            </div>
        </div>
        <div class="PageBlank" style="height: 15px;">
            &nbsp;</div>
        <div id="page2" class="page">
            <div style="text-align: left; font-weight: bold; font-size: 16px; padding-left: 20px;
                line-height: 25px; padding-top: 10px;">
                第三条 供货数量的验收确认</div>
            <div style="text-align: left; padding-left: 30px; line-height: 25px;">
                1、供货数量的确认：预拌混凝土的供货量以需方现场代表签收的混凝土送货单为准。如需方对供方所供的混凝土是否够量有疑问时，即按《预拌混凝土》（GB14902-2003）标准中的规定，用过磅形式随机抽验供方所供混凝土重量（混凝土取样车不作为抽样车辆，每批次抽检不少于三车，以三车平均值作为评定标准）。如抽验重量结果误差±2%以内，双方互不追究；过磅费用由需方承担；如抽查结果负差大于2%时，则抽检前当日发出的供货数均按抽验结果比例计算供货数量，过磅费用由供方方承担。
                <br />
                &nbsp;&nbsp;&nbsp;&nbsp;不同强度等级预拌混凝土的容重参考表：
                <br />
                <table class="table" border="0" cellspacing="0" cellpadding="0" width="604">
                    <tr>
                        <td width="74" align="center" valign="middle">
                            砼强度
                            <br />
                            等级
                        </td>
                        <td width="48" align="center" valign="middle">
                            C10
                        </td>
                        <td width="48" align="center" valign="middle">
                            C15
                        </td>
                        <td width="48" align="center" valign="middle">
                            C20
                        </td>
                        <td width="48" align="center" valign="middle">
                            C25
                        </td>
                        <td width="48" align="center" valign="middle">
                            C30
                        </td>
                        <td width="48" align="center" valign="middle">
                            C35
                        </td>
                        <td width="48" align="center" valign="middle">
                            C40
                        </td>
                        <td width="48" align="center" valign="middle">
                            C45
                        </td>
                        <td width="48" align="center" valign="middle">
                            C50
                        </td>
                        <td width="48" align="center" valign="middle">
                            C55
                        </td>
                        <td width="48" align="center" valign="middle">
                            C60
                        </td>
                    </tr>
                    <tr>
                        <td width="74" align="center" valign="middle">
                            容重
                            <br />
                            kg/m3
                        </td>
                        <td width="48" align="center" valign="middle">
                            2360
                        </td>
                        <td width="48" align="center" valign="middle">
                            2370
                        </td>
                        <td width="48" align="center" valign="middle">
                            2370
                        </td>
                        <td width="48" align="center" valign="middle">
                            2380
                        </td>
                        <td width="48" align="center" valign="middle">
                            2380
                        </td>
                        <td width="48" align="center" valign="middle">
                            2390
                        </td>
                        <td width="48" align="center" valign="middle">
                            2400
                        </td>
                        <td width="48" align="center" valign="middle">
                            2410
                        </td>
                        <td width="48" align="center" valign="middle">
                            2420
                        </td>
                        <td width="48" align="center" valign="middle">
                            2420
                        </td>
                        <td width="48" align="center" valign="middle">
                            2430
                        </td>
                    </tr>
                </table>
                2、供货时间：以实际工程进度需求为准，即以需方通知送货时间供方确认为准。
                <br />
                3、质量验收:<br />
                &nbsp;&nbsp;① 供方提供的混凝土质量应符合《预拌混凝土》（GB14902-2003）、《混凝土强度检验评定标准》（GBJ107-87）及《湖南省预拌混凝土质量管理细则》（试行）的要求；
                <br />
                &nbsp;&nbsp;② 预拌混凝土送货到现场后，需方应对坍落度当场进行检验，并按规定留置试件和养护，并送郴州市有资质的检验机构进行检测，费用由需方负责，检验结果作为验收混凝土质量的依据；<br />
                &nbsp;&nbsp;③ 如需方对产品质量有异议时应书面提出。对坍落度的异议应在交货时提出，对混凝土强度的异议应有国家检验部门的检定后作标准。<br />
                &nbsp;&nbsp;④ 供方在接到需方书面异议后，应及时与需方共同确认质量问题并提出处理方案。当双方不能达成共识是，则请双方共同认可的质监部门进行分析、鉴定和裁决。</div>
            <div style="text-align: left; font-weight: bold; font-size: 16px; padding-left: 20px;
                line-height: 25px; padding-top: 10px;">
                第四条 联系方式</div>
            <div style="text-align: left; padding-left: 30px; line-height: 25px;">
                需方现场联系人：
                <br />
                供方调度室电话：0735-7602220 0735-7602221 15197591630<br />
                传真：0735-7602202 负责人：蒋绍春 13975700583
                <br />
                技术服务电话：0735-7602219 负责人：陈志亮 13037356800<br />
                服务投诉电话：0735-7602229 负责人：谷春林 13975551630</div>
            <div style="text-align: left; font-weight: bold; font-size: 16px; padding-left: 20px;
                line-height: 25px; padding-top: 10px;">
                第五条 供方责任</div>
            <div style="text-align: left; padding-left: 30px; line-height: 25px;">
                1、供方提供的混凝土及使用的原材料，其质量应符合国家GB14902-2003《预拌混凝土》、《湖南省预拌混凝土质量管理细则》（试行）及其所引用的相关技术标准。并按需方的实际浇筑部位向需方提供与预拌混凝土相关的技术资料、出厂质量证明书；
                <br />
                2、按照需方所报计划及具体施工时间准确无误送货到工地，特殊情况不能按时送达，应提前3小时通知需方，协商另行供货时间；
                <br />
                3、供方运送车辆及人员进入现场后，应当服从需方负责人员的统一调度指挥，因司机违反工地规章制度造成的安全责任由供方负责；<br />
                4、供方与需方的质量责任的划分以《湖南省商品混凝土管理细则》规定为准则。</div>
        </div>
        <div class="PageBlank" style="height: 15px;">
            &nbsp;</div>
        <div id="page3" class="page">
            <div style="text-align: left; font-weight: bold; font-size: 16px; padding-left: 20px;
                line-height: 25px; padding-top: 10px;">
                第六条 需方责任</div>
            <div style="text-align: left; padding-left: 30px; line-height: 25px;">
                1、需方应在浇注预拌混凝土的前24小时向供方报需求计划，于每天下午5点前以电话或传真方式将所需混凝土的强度等级、数量、坍落度、浇筑部位、浇筑方式、交货时间及其他有关要求通知供方，如因需方通知不及时或不准确所产生的责任由需方承担；
                <br />
                2、对于泵送用混凝土每次泵送前如须使用的砂浆由需方负责。
                <br />
                3、供货前需方应确保施工现场道路畅通，有必要的供水、照明等设施和停车场地，包括施工范围内的交警、城管、环保部门的审批手续和协调，并安排专人在现场负责调度指挥，保证车辆按环保要求进出工地，若上述条件不能满足，造成供方不能送货或被罚款的其责任由需方承担。
                <br />
                4、需方人员随意加水或加入其他掺和料且导致混凝土达不到质量要求，由需方承担责任，如突遇暴雨、高温天气，需方应采取有效的质量保证措施；
                <br />
                5、混凝土到达工地后，需方应在20分内对混凝土进行检验，并在60分钟内将整车混凝土卸完，若因需方原因超时间不能卸完混凝土供方不负任何质量责任。混凝土运输车卸料超过上述时间的，需方实际情况给予滞留油耗补偿；<br />
                6、需方所用混凝土每天或每批次只能一车不足8m3，如超出此一车数以上，需方应每车支付100元的空载补偿费支付给供方混凝土运输车。</div>
            <div style="text-align: left; font-weight: bold; font-size: 16px; padding-left: 20px;
                line-height: 25px; padding-top: 10px;">
                第七条 违约责任</div>
            <div style="text-align: left; padding-left: 30px; line-height: 25px;">
                1、供方不能满足质量技术要求或不能履行合同条款，需方有权单方终止合同，并追究供方责任；
                <br />
                2、需方未按照合同条款支付货款，每逾期壹天，欠款总额每天千分之五向供方支付违约金，供方同时有权终止供货并单方面解除本合同。<br />
                3、供、需双方任何一方违约时，所产生的律师费、差旅费、评估鉴定费、公告费、诉讼费、保全费等费用由违约方承担。</div>
            <div style="text-align: left; font-weight: bold; font-size: 16px; padding-left: 20px;
                line-height: 25px; padding-top: 10px;">
                第八条 其他</div>
            <div style="text-align: left; padding-left: 30px; line-height: 25px;">
                1、需方要求掺入特殊添加剂时，由需方提供并送到供方搅拌站。若提供的特殊添加剂为粉剂，则供方向需方收取按每立方10元加工费。如需方委托供方购买特殊添加剂，则单价另行协商。
                <br />
                2、需方用电话通知供方送料时，至少要重复两次确认所报混凝土标号、数量、浇灌部位、到达时间等内容，以供方电话记录为准。
                <br />
                3、本合同须经过双方法定代表人或法定代表人授权签约人签字，供需双方应相互加盖公章，没有公章须提供本人身份证复印件，
                <br />
                4、在合同履行过程中，任何一方发生争议时，双方应进行友好协商，协商或调解解决不成的，可向当地人民法院提起诉讼；
                <br />
                5、本合同未尽事宜，经双方协商一致另行补充约定。补充约定与本合同具有同等法律效力。
                <br />
                6、本合同自供、需双方签订本合同之日起生效至需方付清供方所有货款后失效。
                <br />
                7、本合同一式贰份，双方各执壹份，均具有同等法律效力。<br />
            </div>
        </div>
        <div class="PageNext">
        </div>
        <div class="PageBlank" style="height: 15px;">
            &nbsp;</div>
        <div id="page4" class="page">
            <div style="text-align: left; padding-left: 30px; line-height: 25px;">
                <table class="table" border="0" cellspacing="0" cellpadding="0" width="588">
                    <tr>
                        <td width="72" colspan="2" align="center" valign="middle">
                            供 方<br />
                            （公章）
                        </td>
                        <td width="216" colspan="3" align="center" valign="middle">
                            <input type="text" id="SupplyUnitName3" name="SupplyUnitName" style="width: 200px;
                                text-decoration: none; font-size: 14px; padding-left: 12px; letter-spacing: 1px;"
                                value="郴州泰湘混凝土有限公司" />
                        </td>
                        <td width="72" align="center" valign="middle">
                            需 方<br />
                            （公章）
                        </td>
                        <td width="228" colspan="3" align="center" valign="middle">
                            <input type="text" id="CustomerName3" name="CustomerName" style="width: 210px; text-decoration: none;
                                font-size: 14px; padding-left: 12px; letter-spacing: 1px;" value="" />
                        </td>
                    </tr>
                    <tr>
                        <td width="72" colspan="2" rowspan="2" align="center" valign="middle">
                            法定<br />
                            代表人
                        </td>
                        <td width="60" rowspan="2" align="center" valign="middle">
                            <input type="text" name="ALegal" id="ALegal" style="width: 50px;" />
                        </td>
                        <td width="72" align="center" valign="middle">
                            签约代表
                        </td>
                        <td width="84" align="center" valign="middle">
                            <input type="text" name="ASigner" id="ASigner" style="width: 60px;" />
                        </td>
                        <td width="72" rowspan="2" align="center" valign="middle">
                            法定<br />
                            代表人
                        </td>
                        <td width="60" rowspan="2" align="center" valign="middle">
                            <input type="text" name="BLegal" id="BLegal" style="width: 50px;" />
                        </td>
                        <td width="84" align="center" valign="middle">
                            签约代表
                        </td>
                        <td width="84" align="center" valign="middle">
                            <input type="text" name="BSigner" id="BSigner" style="width: 60px;" />
                        </td>
                    </tr>
                    <tr>
                        <td width="72" align="center" valign="middle">
                            经办人
                        </td>
                        <td width="84" align="center" valign="middle">
                            <input type="text" name="AManager" id="AManager" style="width: 60px;" />
                        </td>
                        <td width="84" align="center" valign="middle">
                            经办人
                        </td>
                        <td width="84" align="center" valign="middle">
                            <input type="text" name="BManager" id="BManager" style="width: 60px;" />
                        </td>
                    </tr>
                    <tr>
                        <td width="288" colspan="5" align="left" valign="middle">
                            &nbsp;地址：<input type="text" name="AlinkAddr" id="AlinkAddr" value="郴州市北湖区石盖塘镇工业园"
                                style="width: 220px;" />
                        </td>
                        <td width="300" colspan="4" align="left" valign="middle">
                            &nbsp;联系地址：<input type="text" name="BlinkAddr" id="BlinkAddr" style="width: 215px;" />
                        </td>
                    </tr>
                    <tr>
                        <td width="288" colspan="5" align="left" valign="middle">
                            &nbsp;开户名：
                            <input type="text" name="ABankName" id="ABankName" style="width: 200px;" value="郴州泰湘混凝土有限公司" />
                            <br />
                            &nbsp;开户行：
                            <input type="text" name="ABank" id="ABank" style="width: 200px;" value="郴州市建行东风支行" />
                            <br />
                            &nbsp;帐&nbsp;&nbsp;号：
                            <input type="text" name="ABankAccount" id="ABankAccount" style="width: 200px;" value="4300 1501 5700 5250 0950" />
                        </td>
                        <td width="300" colspan="4" align="left" valign="middle">
                            &nbsp;开户名：
                            <input type="text" name="BBankName" id="BBankName" style="width: 220px;" />
                            <br />
                            &nbsp;开户行：
                            <input type="text" name="BBank" id="BBank" style="width: 220px;" />
                            <br />
                            &nbsp;帐&nbsp;&nbsp;号：
                            <input type="text" name="BBankAccount" id="BBankAccount" style="width: 220px;" />
                        </td>
                    </tr>
                    <tr>
                        <td width="60" align="center" valign="middle">
                            需方
                            <br />
                            提供
                            <br />
                            的担保
                        </td>
                        <td width="528" colspan="8" align="left" valign="middle">
                            &nbsp;担保方式：无限连带责任保证担保
                            <br />
                            &nbsp;担保范围：若需方未按本合同支付或未足额支付供方货款，则担保人须对需方未付的货款、违约金及本合同第十条第4项所列费用承担无限连带担保清偿责任，即担保人代需方支付前述全部款项。
                            <br />
                            &nbsp;担保期限：自需方逾期付款之日起二年。 需方担保人（公章）： 签名：
                            <br />
                            &nbsp;担保人地址：
                            <br />
                            &nbsp;开户名：
                            <input type="text" name="DBBankName" id="DBBankName" style="width: 230PX;" />
                            <br />
                            &nbsp;开户行：
                            <input type="text" name="DBBank" id="DBBank" style="width: 230PX;" />
                            <br />
                            &nbsp;帐&nbsp;&nbsp;号：
                            <input type="text" name="DBBankAccount" id="DBBankAccount" style="width: 230PX;" />
                        </td>
                    </tr>
                </table>
            </div>
            <div style="text-align: left; padding-left: 30px; padding-top: 10px; padding-bottom: 5px;">
                签约时间：<input type="text" id="signY" name="signY" style="width: 40px; text-align: center;" />年<input
                    type="text" id="signM" name="signM" style="width: 25px; text-align: center;" />月<input
                        type="text" id="signD" name="signD" style="width: 25px; text-align: center;" />日&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;签约地点：<input
                            type="text" id="signAddr" name="signAddr" style="width: 251px; text-align: left;" />
            </div>
        </div>
    </div>
</body>
</html>
