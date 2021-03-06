/*DOA分析内容存储*/
CREATE TABLE doa_analysis(
Id INT PRIMARY KEY IDENTITY, 
_8s NVARCHAR(128) NOT NULL,
response_check NVARCHAR(128) NOT NULL,
analysis_step NVARCHAR(128) NOT NULL,
improve_action NVARCHAR(128) NOT NULL,
inputer NVARCHAR(128) NOT NULL,
input_date date
)


/*新旧8s对应关系表*/
CREATE TABLE old_new_8s_relationship(
Id INT PRIMARY KEY IDENTITY, 
old8s NVARCHAR(128) NOT NULL,
new8s NVARCHAR(128) NOT NULL,
inputer NVARCHAR(128) NOT NULL,
input_date date
)


/*后台管理表*/
CREATE TABLE functionControltable(
Id INT PRIMARY KEY IDENTITY, 
funtion NVARCHAR(128) NOT NULL,/*功能名称*/
able NVARCHAR(128) NOT NULL, /*是否启用，默认启用 1 不启用 0*/
other NVARCHAR(128),/*备用*/
)

/*Obe 抽查的结果表 */
CREATE TABLE ObeStationtable(
Id INT PRIMARY KEY IDENTITY, 
track_serial_no NVARCHAR(128) NOT NULL,
orderno NVARCHAR(128) NOT NULL, /*订单编号*/
custom_materialNo NVARCHAR(128) NOT NULL,/*客户料号*/
checkresult NVARCHAR(128),/*检查结果，F fail ： P pass*/
failreason NVARCHAR(1280),
tester NVARCHAR(128) NOT NULL,
input_date date
)

/*收货一站来决定是否obe抽检，采用总数与抽查比例算后用等差数列来计数 */
CREATE TABLE decideOBEchecktable(
Id INT PRIMARY KEY IDENTITY, 
track_serial_no NVARCHAR(128) NOT NULL,
orderno NVARCHAR(128) NOT NULL, /*订单编号*/
custom_materialNo NVARCHAR(128) NOT NULL,/*客户料号*/
totalNum NVARCHAR(128),
rate NVARCHAR(128),/*抽查比例*/
currentindex NVARCHAR(128),/*在总数里面的序数第几个，先到先计数，顺序不固定*/
ischeck NVARCHAR(128),/*是否抽查*/
tester NVARCHAR(128) NOT NULL,
input_date date
)

/*
订单的抽检比例，根据订单号来做区分，如果已经开始走obe则不能修改抽检比例，否在可以修改
*/
CREATE TABLE obe_checkrate_table(
Id INT PRIMARY KEY IDENTITY, 
orderno NVARCHAR(128) NOT NULL, /*订单编号*/
custom_materialNo NVARCHAR(128) NOT NULL,/*客户料号*/
rate  NVARCHAR(128) NOT NULL, 
inputer NVARCHAR(128) NOT NULL, /*输入人*/
input_date date, /*日期*/
)



/*测试结果表格， 包括：序列号，测试人， 时间， 结果（Pass/Fail)， 如果是错误的必须要错误描述， 站别*/

CREATE TABLE test_all_result_record(
Id INT PRIMARY KEY IDENTITY,
trackno NVARCHAR(128), /*序列号*/
tester NVARCHAR(128) NOT NULL, /*测试人*/
inputdate datetime NOT NULL, /*时间*/
result NVARCHAR(128) NOT NULL, /*结果*/
failDescribe NVARCHAR(256),/*错误原因*/
station NVARCHAR(128) not null
)


/*主板生命周期表*/
CREATE TABLE [dbo].[stationInfoRecord](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[trackno] [nvarchar](128) NOT NULL,
	[station] [nvarchar](128) NOT NULL,
	[inputer] [nvarchar](128) NOT NULL,
	[inputdate] [datetime] NOT NULL
) ON [PRIMARY]

/*主板拦截信息导入*/
CREATE TABLE mb_receive_check(
Id INT PRIMARY KEY IDENTITY,
vendor NVARCHAR(128), /*厂商*/
product NVARCHAR(128) NOT NULL, /*客户别*/
custom_machine_type NVARCHAR(128) NOT NULL, /*客户机型*/
mb_brief NVARCHAR(128) NOT NULL, /*MB简称*/
custom_material_no NVARCHAR(128),/*客户料号*/
custom_serial_no NVARCHAR(128) NOT NULL, /*客户序号*/
vendor_serail_no NVARCHAR(128) NOT NULL, /*厂商序号*/
vendor_material_no NVARCHAR(128),/*厂商料号*/
mb_describe NVARCHAR(128) NOT NULL,/*MB描述*/
check_reason NVARCHAR(128) NOT NULL,/*拦截原因*/
)

/*记录打印8s的数据*/
CREATE TABLE print_8s_record(
Id INT PRIMARY KEY IDENTITY, 
track_serial_no NVARCHAR(128) NOT NULL, /*跟踪条码*/
_8sCode NVARCHAR(128) NOT NULL, /*8s*/
inputer NVARCHAR(128) NOT NULL, /*操作人*/
input_date date, /*输入日期*/
)

/*记录cpu的sn在库房换料的数据*/
CREATE TABLE old_cpu_sn_record(
Id INT PRIMARY KEY IDENTITY, 
track_serial_no NVARCHAR(128) NOT NULL, /*跟踪条码*/
oldsn NVARCHAR(128) NOT NULL, /*订单编号*/
inputer NVARCHAR(128) NOT NULL, /*操作人*/
input_date date, /*输入日期*/
)

/*二次或多次根据条件锁定*/
CREATE TABLE need_to_lock(
Id INT PRIMARY KEY IDENTITY, 
locktype NVARCHAR(128),/*analysis_8s,modify_more_than_three, ntf_twice*/
track_serial_no NVARCHAR(128) NOT NULL, /*跟踪条码*/
orderno NVARCHAR(128) NOT NULL, /*订单编号*/
_8sCode NVARCHAR(128), /*8s*/
isLock NVARCHAR(128), /*默认false*/
input_date date, /*输入日期*/
unlcok_date date, /*解锁日期，为true时此日期不为空*/
)

/*日期	厂商	材料类别	MPN	MB简称	简述	描述	状态	数量	出库原因	收货商名称	备注*/
CREATE TABLE TransferOrSold_sheet(
Id INT PRIMARY KEY IDENTITY, 
vendor NVARCHAR(128), /*厂商*/
material_type NVARCHAR(128), /*材料类别MB/BGA/FRU/SMT*/
bga_type NVARCHAR(128), /*bga与null*/
mpn NVARCHAR(128), /*MPN*/
mb_brief NVARCHAR(128), /*MB简称*/
other_brief NVARCHAR(128), /*简述-除mb外的简称,mb的话为空*/
describe NVARCHAR(128), /*描述*/
_state NVARCHAR(128), /*状态,良品不良品*/
number NVARCHAR(128), /*数量*/
out_reason NVARCHAR(128), /*出库原因*/
receiver NVARCHAR(128), /*收货商名称*/
note NVARCHAR(128), /*备注*/
inputer NVARCHAR(128) NOT NULL, /*操作人*/
input_date NVARCHAR(128), /*日期*/
)

/*年份	厂商	客户库别	客户别	客户料号	MPN	MB简称	汇总数量	月1	月2	月3	月4	月5	月6	月7	月8	月9	月10	月11	月12*/
CREATE TABLE repaire_history_data_sheet(
Id INT PRIMARY KEY IDENTITY, 
year_ NVARCHAR(128), /*年份*/
vendor NVARCHAR(128) NOT NULL, /*厂商*/
house NVARCHAR(128) NOT NULL, /*库别*/
product NVARCHAR(128) NOT NULL, /*客户别*/
custom_material_no NVARCHAR(128),/*客户料号*/
mpn NVARCHAR(128) NOT NULL, /*MPN*/
mb_brief NVARCHAR(128) NOT NULL, /*MB简称*/
sum_year NVARCHAR(128),/*年汇总*/
sum_1 NVARCHAR(128),/*1月汇总*/
sum_2 NVARCHAR(128),/*2月汇总*/
sum_3 NVARCHAR(128),/*3月汇总*/
sum_4 NVARCHAR(128),/*4月汇总*/
sum_5 NVARCHAR(128),/*5月汇总*/
sum_6 NVARCHAR(128),/*6月汇总*/
sum_7 NVARCHAR(128),/*7月汇总*/
sum_8 NVARCHAR(128),/*8月汇总*/
sum_9 NVARCHAR(128),/*9月汇总*/
sum_10 NVARCHAR(128),/*10月汇总*/
sum_11 NVARCHAR(128),/*11月汇总*/
sum_12 NVARCHAR(128),/*12月汇总*/
)

/*整机出货量，从联想拿到的数据，用来计算维修比例的基数*/
CREATE TABLE whole_import_sheet(
Id INT PRIMARY KEY IDENTITY, 
vendor NVARCHAR(128) NOT NULL, /*厂商*/
product NVARCHAR(128) NOT NULL, /*客户别*/
mb_brief NVARCHAR(128) NOT NULL, /*MB简称*/
custom_machine_type NVARCHAR(128) NOT NULL, /*客户机型*/
machine_pn NVARCHAR(128) NOT NULL, /*整机PN*/
machine_describe NVARCHAR(128) NOT NULL, /*整机描述*/
mpn NVARCHAR(128) NOT NULL, /*MPN*/
mb_describe NVARCHAR(128) NOT NULL,/*MB描述*/
out_number NVARCHAR(128) NOT NULL,/*出货量*/
out_date  date,/*出货日期*/
gurrante_period  NVARCHAR(128) NOT NULL,/*保修期*/
upload_ddate  date,/*上传日期*/
buy_type  NVARCHAR(128) NOT NULL,/*材料采购类别*/
note  NVARCHAR(128),/*备注*/
)

/*不良品SMT/BGA入库记录*/
CREATE TABLE badcodes(
Id INT PRIMARY KEY IDENTITY, 
yimaicode NVARCHAR(128),/*料号*/
yimaides NVARCHAR(128), /*入库数量*/
renbaocode NVARCHAR(128),/*料号*/
renbaodes NVARCHAR(128), /*入库数量*/
)


CREATE TABLE frubomtable(
Id INT PRIMARY KEY IDENTITY, 
vendor NVARCHAR(128),/*厂商*/
product NVARCHAR(128),/*客户别*/
custom_material_no NVARCHAR(128) NOT NULL,/*客户料号*/
replace_material NVARCHAR(128),/*可替代料号*/
machine_type NVARCHAR(128),/*机型*/
name NVARCHAR(128),/*名称*/
custommaterialdescribe NVARCHAR(128),/*客户物料描述*/
vendor_material_no NVARCHAR(128),/*厂商料号*/
mpn1 NVARCHAR(128),/*MPN1*/
mpn1_des NVARCHAR(128),/*MPN1描述*/
mpn2 NVARCHAR(128),/*MPN2*/
mpn2_des NVARCHAR(128),/*MPN2描述*/
mpn3 NVARCHAR(128),/*MPN3*/
mpn3_des NVARCHAR(128),/*MPN3描述*/
mpn4 NVARCHAR(128),/*MPN4*/
mpn4_des NVARCHAR(128),/*MPN4描述*/
gurantee NVARCHAR(128),/*保修期*/
eol NVARCHAR(128),/*EOL*/
input_date date,/*添加日期*/
)


CREATE TABLE limit_gurante(
Id INT PRIMARY KEY IDENTITY, 
import_date	date,
MB_CUST_SN	NVARCHAR(128),
MB_FRU_PN	NVARCHAR(128),
MB_COMPAL_PN	NVARCHAR(128),
MB_COMPAL_SN	NVARCHAR(128),
SERIAL_NUMBER	NVARCHAR(128),
MODEL_NAME	NVARCHAR(128),
PRODUCT_DESC	NVARCHAR(128),
DUE_DATE	date, 
serial NVARCHAR(128),
)

/*不良品MB/SMT/BGA出库记录*/
CREATE TABLE mb_smt_bga_ng_out_house_table(
Id INT PRIMARY KEY IDENTITY, 
mpn NVARCHAR(128) NOT NULL,/*料号*/
in_number NVARCHAR(128), /*入库数量*/
input_date date, /*输入日期*/
declare_unit NVARCHAR(128), /*申报单位,需要转换*/
declare_number NVARCHAR(128), /*报关单号*/
custom_request_number NVARCHAR(128), /*申请单号*/
)

/*不良品SMT/BGA入库记录*/
CREATE TABLE smt_bga_ng_in_house_table(
Id INT PRIMARY KEY IDENTITY, 
mpn NVARCHAR(128) NOT NULL,/*料号*/
in_number NVARCHAR(128), /*入库数量*/
input_date date, /*输入日期*/
)

/*SMT/BGA不良品库房信息 把MB不良品库也放入一起*/
CREATE TABLE store_house_ng(
Id INT PRIMARY KEY IDENTITY, 
house NVARCHAR(128), /*库房*/
place NVARCHAR(128), /*储位,储位的名称开始字母可以区分可以存储的类型，fru/smt, bga, mb*/
mpn NVARCHAR(128), /*存储料号(如果是MB则对应客户料号)*/
number NVARCHAR(128), /*已存数量,不限数量的，可以累积*/
)

CREATE TABLE store_house_ng_in(
Id INT PRIMARY KEY IDENTITY, 
house NVARCHAR(128), /*库房*/
place NVARCHAR(128), /*储位,储位的名称开始字母可以区分可以存储的类型，fru/smt, bga, mb*/
mpn NVARCHAR(128), /*存储料号(如果是MB则对应客户料号)*/
number NVARCHAR(128), /*已存数量,不限数量的，可以累积*/
inputer NVARCHAR(128) NOT NULL, /*入库人*/
input_date date, /*输入日期*/
)

CREATE TABLE store_house_ng_out(
Id INT PRIMARY KEY IDENTITY, 
house NVARCHAR(128), /*库房*/
place NVARCHAR(128), /*储位,储位的名称开始字母可以区分可以存储的类型，fru/smt, bga, mb*/
mpn NVARCHAR(128), /*存储料号(如果是MB则对应客户料号)*/
number NVARCHAR(128), /*已存数量,不限数量的，可以累积*/
inputer NVARCHAR(128) NOT NULL, /*入库人*/
input_date date, /*输入日期*/
)

/*MB不良品出库表记录*/
/*CREATE TABLE fault_mb_out_record_table(
Id INT PRIMARY KEY IDENTITY, 
track_serial_no NVARCHAR(128) NOT NULL, /*跟踪条码*/
vendor NVARCHAR(128) NOT NULL, /*厂商*/
product NVARCHAR(128) NOT NULL, /*客户别*/
source NVARCHAR(128) NOT NULL, /*来源*/
orderno NVARCHAR(128) NOT NULL, /*订单编号*/
receivedate date, /*收货日期*/
mb_describe NVARCHAR(128), /*MB描述*/
mb_brief NVARCHAR(128), /*MB简称*/
custom_serial_no NVARCHAR(128) NOT NULL, /*客户序号*/
vendor_serail_no NVARCHAR(128) NOT NULL, /*厂商序号*/
mpn NVARCHAR(128) NOT NULL, /*MPN*/
mb_make_date date, /*MB生产日期*/
customFault NVARCHAR(128) NOT NULL, /*客户故障*/
ECO NVARCHAR(128), /*ECO*/
repairer NVARCHAR(128) NOT NULL, /*维修人*/
repair_date date, /*修复日期*/
)*/
/*存储文件数据库*/
CREATE TABLE TestCpu(
    id varchar(10),
    cpupn image,
    chkcpu image 
)

/*不良品入库表*/
CREATE TABLE fault_mb_enter_record_table(
Id INT PRIMARY KEY IDENTITY, 
track_serial_no NVARCHAR(128) NOT NULL, /*跟踪条码*/
vendor_sn NVARCHAR(128) NOT NULL, /*厂商sn*/
vendor NVARCHAR(128) NOT NULL, /*厂商*/
product NVARCHAR(128) NOT NULL, /*客户别*/
mb_brief NVARCHAR(128), /*MB简称*/
mpn NVARCHAR(128) NOT NULL, /*MPN*/
mb_describe NVARCHAR(128), /*MB描述*/

_status NVARCHAR(128), /**/
pch_brief NVARCHAR(128), /*PCH描述*/
vga_brief NVARCHAR(128), /*VGA描述*/
cpu_brief NVARCHAR(128), /*CPU描述*/

repairer NVARCHAR(128) NOT NULL, /*入库人*/
repair_date date, /*入库日期*/
)

/*不良品主板确认表*/
CREATE TABLE fault_mb_confirm_table(
Id INT PRIMARY KEY IDENTITY, 
track_serial_no NVARCHAR(128) NOT NULL, /*跟踪条码*/
vendor_sn NVARCHAR(128) NOT NULL, /*厂商sn*/
vendor NVARCHAR(128) NOT NULL, /*厂商*/
product NVARCHAR(128) NOT NULL, /*客户别*/
mb_brief NVARCHAR(128), /*MB简称*/
mpn NVARCHAR(128) NOT NULL, /*MPN*/
mb_describe NVARCHAR(128), /*MB描述*/

fault_describe NVARCHAR(128), /*不良现象*/
fault_place NVARCHAR(128), /*不良位置*/
fault_reason NVARCHAR(128), /*报废原因*/

confirmer NVARCHAR(128) NOT NULL, /*判定人*/
confirm_date date, /*判定日期*/
pcbzhouqi NVARCHAR(128), /*pcb周期*/
pcbtype NVARCHAR(128), /*pcb型号*/
)


/*库房信息*/
CREATE TABLE store_house(
Id INT PRIMARY KEY IDENTITY, 
house NVARCHAR(128), /*库房*/
place NVARCHAR(128), /*储位,储位的名称开始字母可以区分可以存储的类型，fru/smt, bga, mb*/
mpn NVARCHAR(128), /*存储料号*/
number NVARCHAR(128), /*已存数量,不限数量的，可以累积*/
)

/*请求归还料表 库房*/
CREATE TABLE fru_smt_return_store_record(
Id INT PRIMARY KEY IDENTITY, 
material_mpn NVARCHAR(128), /*材料mpn*/
return_number NVARCHAR(128), /*请求数量*/
stock_place NVARCHAR(128),/*库位*/

requester NVARCHAR(128), /*请求人*/
request_date date, /*请求日期*/

processer NVARCHAR(128), /*处理人*/
processe_date date, /*处理日期*/
_status NVARCHAR(128), /*request/done 2中状态*/
fromId NVARCHAR(128),/*从request_fru_smt_to_store_table中过来的id，还需要此id来更新状态*/
)

/*需要创建一个表格，记录维修的每条使用记录 TODO...*/
CREATE TABLE fru_smt_used_record(
Id INT PRIMARY KEY IDENTITY, 
inputer  NVARCHAR(128),/*输入人*/
input_date  date,/*日期*/
track_serial_no NVARCHAR(128),/*跟踪条码*/

material_mpn NVARCHAR(128), /*材料mpn*/
thisNumber NVARCHAR(128), /*此次使用的数量*/
stock_place NVARCHAR(128),/*库位*/
isUsed NVARCHAR(128),/*是否是拆件*/
_action NVARCHAR(128),/*对应动作*/
)

CREATE TABLE request_fru_smt_to_store_table(
Id INT PRIMARY KEY IDENTITY, 
mb_brief NVARCHAR(1280), /*机型*/
not_good_place NVARCHAR(128), /*不良位置*/
material_mpn NVARCHAR(128), /*材料mpn*/
material_describe NVARCHAR(128), /*材料描述*/
number NVARCHAR(128), /*请求数量*/
realNumber NVARCHAR(128), /*获得的真正数量*/
requester NVARCHAR(128), /*请求人*/
_date date, /*请求日期*/
_status NVARCHAR(128), /*状态, request/close/part/wait,return*/
usedNumber NVARCHAR(128), /*使用的数量,是个累加数量*/
stock_place NVARCHAR(128),/*库位->剩余数量*/
processer NVARCHAR(128), /*处理人*/
processe_date date, /*处理日期*/
)

CREATE TABLE mb_out_stock(
Id INT PRIMARY KEY IDENTITY, 
track_serial_no NVARCHAR(128) NOT NULL, /*跟踪条码*/
vendor NVARCHAR(128) NOT NULL, /*厂商*/
product NVARCHAR(128) NOT NULL, /*客户别*/
mpn NVARCHAR(128) NOT NULL, /*MPN*/
mb_brief NVARCHAR(128) NOT NULL,/*MB简称*/
describe NVARCHAR(128) NOT NULL,/*描述*/
custom_serial_no NVARCHAR(128) NOT NULL,/*客户序号*/
vendor_serial_no NVARCHAR(128) NOT NULL,/*厂商序号*/
vendormaterialNo NVARCHAR(128) NOT NULL,/*厂商料号*/
custommaterialNo NVARCHAR(128) NOT NULL,/*客户料号->输入*/
dpk_type NVARCHAR(128),/*DPK类型 物料对照表带出*/
dpkpn NVARCHAR(128),/*DPKPN 物料对照表带出*/
stock_place NVARCHAR(128),/*库位*/
note NVARCHAR(128),/*备注*/
taker NVARCHAR(128),/*领用人*/
inputer  NVARCHAR(128),/*输入人*/
input_date date,/*日期*/
)

CREATE TABLE mb_in_stock(
Id INT PRIMARY KEY IDENTITY, 
buy_order_serial_no NVARCHAR(128) NOT NULL, /*采购订单编号*/
vendor NVARCHAR(128) NOT NULL, /*厂商*/
buy_type NVARCHAR(128) NOT NULL, /*采购类别*/
product NVARCHAR(128) NOT NULL, /*客户别*/
material_type NVARCHAR(128) NOT NULL, /*材料大类*/
mpn NVARCHAR(128) NOT NULL, /*MPN*/
vendormaterialNo NVARCHAR(128) NOT NULL,/*厂商料号*/
describe NVARCHAR(128) NOT NULL,/*描述*/

number  NVARCHAR(128) NOT NULL,/*订单数量*/
input_number  NVARCHAR(128),/*入库数量*/
stock_place NVARCHAR(128),/*库位*/

pricePer NVARCHAR(128) NOT NULL,/*单价*/

mb_brief NVARCHAR(128) NOT NULL,/*MB简称*/
custom_serial_no NVARCHAR(128) NOT NULL,/*客户序号*/
vendor_serial_no NVARCHAR(128) NOT NULL,/*厂商序号*/
track_serial_no NVARCHAR(128) NOT NULL, /*跟踪条码*/

note NVARCHAR(128),/*备注*/
inputer  NVARCHAR(128),/*输入人*/
input_date  date,/*日期*/
)

CREATE TABLE bga_out_stock(
Id INT PRIMARY KEY IDENTITY, 
vendor NVARCHAR(128) NOT NULL, /*厂商*/
product NVARCHAR(128) NOT NULL, /*客户别*/
mpn NVARCHAR(128) NOT NULL, /*MPN*/
bga_brief NVARCHAR(128) NOT NULL,/*BGA简称*/
bga_describe NVARCHAR(128) NOT NULL,/*BGA描述*/

stock_place NVARCHAR(128),/*库位*/
out_number  NVARCHAR(128),/*出库数量*/
note NVARCHAR(128),/*备注*/

taker NVARCHAR(128),/*领用人*/
inputer  NVARCHAR(128),/*输入人*/
input_date date,/*日期*/
)

CREATE TABLE bga_in_stock(
Id INT PRIMARY KEY IDENTITY, 
buy_order_serial_no NVARCHAR(128) NOT NULL, /*采购订单编号*/
vendor NVARCHAR(128) NOT NULL, /*厂商*/
buy_type NVARCHAR(128) NOT NULL, /*采购类别*/
product NVARCHAR(128) NOT NULL, /*客户别*/
material_type NVARCHAR(128) NOT NULL, /*材料大类*/
mpn NVARCHAR(128) NOT NULL, /*MPN*/
vendormaterialNo NVARCHAR(128) NOT NULL,/*厂商料号*/
describe NVARCHAR(128) NOT NULL,/*描述*/
pricePer NVARCHAR(128) NOT NULL,/*单价*/

bga_describe NVARCHAR(128) NOT NULL,/*BGA简述*/
order_number NVARCHAR(128),/*订单数量*/
input_number  NVARCHAR(128),/*入库数量*/
bgasn NVARCHAR(128),/*BGASN*/
stock_place NVARCHAR(128),/*库位*/
note NVARCHAR(128),/*备注*/
inputer  NVARCHAR(128),/*输入人*/
input_date date,/*日期*/
)


CREATE TABLE fru_smt_out_stock(
Id INT PRIMARY KEY IDENTITY, 
vendor NVARCHAR(128) NOT NULL, /*厂商*/
buy_type NVARCHAR(128) NOT NULL, /*采购类别*/
product NVARCHAR(128) NOT NULL, /*客户别*/
material_type NVARCHAR(128) NOT NULL, /*材料大类*/
mpn NVARCHAR(128) NOT NULL, /*MPN*/
mb_brief NVARCHAR(1280) NOT NULL,/*MB简称*/
material_name  NVARCHAR(128) NOT NULL,/*材料名称*/
vendormaterialNo NVARCHAR(128) NOT NULL,/*厂商料号*/
describe NVARCHAR(128) NOT NULL,/*描述*/
stock_out_num  NVARCHAR(128),/*出库数量*/
pricePer NVARCHAR(128) NOT NULL,/*单价*/
stock_place NVARCHAR(128),/*库位*/
taker NVARCHAR(128),/*领用人*/
inputer  NVARCHAR(128),/*输入人*/
use_describe NVARCHAR(128),/*用途*/
note NVARCHAR(128),/*备注*/
input_date date,/*日期*/
)

CREATE TABLE fru_smt_in_stock(
Id INT PRIMARY KEY IDENTITY, 
buy_order_serial_no NVARCHAR(128) NOT NULL, /*采购订单编号*/
vendor NVARCHAR(128) NOT NULL, /*厂商*/
buy_type NVARCHAR(128) NOT NULL, /*采购类别*/
product NVARCHAR(128) NOT NULL, /*客户别*/
material_type NVARCHAR(128) NOT NULL, /*材料大类*/
mpn NVARCHAR(128) NOT NULL, /*MPN*/
vendormaterialNo NVARCHAR(128) NOT NULL,/*厂商料号*/
describe NVARCHAR(128) NOT NULL,/*描述*/
number  NVARCHAR(128) NOT NULL,/*订单数量*/
pricePer NVARCHAR(128) NOT NULL,/*单价*/

mb_brief NVARCHAR(1280) NOT NULL,/*MB简称*/
material_name  NVARCHAR(128) NOT NULL,/*材料名称*/
stock_in_num  NVARCHAR(128),/*入库数量*/
totalMoney  NVARCHAR(128),/*金额合计*/
stock_place NVARCHAR(128),/*库位*/
note NVARCHAR(128),/*备注*/
inputer  NVARCHAR(128),/*输入人*/
input_date date,/*日期*/
)

/*材料入库单*/
CREATE TABLE stock_in_sheet(
Id INT PRIMARY KEY IDENTITY, 
buy_order_serial_no NVARCHAR(128) NOT NULL, /*采购订单编号*/
vendor NVARCHAR(128) NOT NULL, /*厂商*/
buy_type NVARCHAR(128) NOT NULL, /*采购类别*/
product NVARCHAR(128) NOT NULL, /*客户别*/
material_type NVARCHAR(128) NOT NULL, /*材料大类*/
mpn NVARCHAR(128) NOT NULL, /*MPN*/
vendormaterialNo NVARCHAR(128) NOT NULL,/*厂商料号*/
describe NVARCHAR(128) NOT NULL,/*描述*/
number  NVARCHAR(128) NOT NULL,/*订单数量*/
pricePer  NVARCHAR(128) NOT NULL,/*单价*/
material_name  NVARCHAR(128) NOT NULL,/*材料名称*/
totalMoney  NVARCHAR(128),/*金额合计*/
stock_in_num  NVARCHAR(128),/*入库数量*/

_status NVARCHAR(128),/*状态 open/close*/

inputer  NVARCHAR(128),/*输入人*/
input_date  date,/*日期*/
)

/*bga待料记录*/
CREATE TABLE bga_wait_material_record_table(
Id INT PRIMARY KEY IDENTITY, 
track_serial_no NVARCHAR(128) NOT NULL, /*跟踪条码*/
customMaterialNo NVARCHAR(128) NOT NULL, /*客户料号*/
vendor NVARCHAR(128) NOT NULL, /*厂商*/
product NVARCHAR(128) NOT NULL, /*客户别*/
source NVARCHAR(128) NOT NULL, /*来源*/
orderno NVARCHAR(128) NOT NULL, /*订单编号*/
receivedate date, /*收货日期*/
mb_describe NVARCHAR(128), /*MB描述*/
mb_brief NVARCHAR(128), /*MB简称*/
custom_serial_no NVARCHAR(128) NOT NULL, /*客户序号*/
vendor_serail_no NVARCHAR(128) NOT NULL, /*厂商序号*/
mpn NVARCHAR(128) NOT NULL, /*MPN*/
mb_make_date date, /*MB生产日期*/
customFault NVARCHAR(128) NOT NULL, /*客户故障*/
ECO NVARCHAR(128), /*ECO*/
bgatype NVARCHAR(128), /*bga类型*/
BGAPN NVARCHAR(128), /*BGAPN*/
BGA_describe NVARCHAR(128), /*BGA描述*/
bga_brief NVARCHAR(128) , /*BGA简述*/
inputer NVARCHAR(128) NOT NULL, /*bga维修人*/
input_date date, /*bga维修日期*/
status NVARCHAR(128) , /*板子是否还待料，如果待料则为空，否无设置为1*/
)

/*bga待维修记录*/
CREATE TABLE bga_wait_record_table(
Id INT PRIMARY KEY IDENTITY, 
track_serial_no NVARCHAR(128) NOT NULL, /*跟踪条码*/
_status NVARCHAR(128) NOT NULL, /*BGA当前的状态*/
vendor NVARCHAR(128) NOT NULL, /*厂商*/
product NVARCHAR(128) NOT NULL, /*客户别*/
source NVARCHAR(128) NOT NULL, /*来源*/
orderno NVARCHAR(128) NOT NULL, /*订单编号*/
receivedate date, /*收货日期*/
mb_describe NVARCHAR(128), /*MB描述*/
mb_brief NVARCHAR(128), /*MB简称*/
custom_serial_no NVARCHAR(128) NOT NULL, /*客户序号*/
vendor_serail_no NVARCHAR(128) NOT NULL, /*厂商序号*/
mpn NVARCHAR(128) NOT NULL, /*MPN*/
mb_make_date date, /*MB生产日期*/
customFault NVARCHAR(128) NOT NULL, /*客户故障*/
ECO NVARCHAR(128), /*ECO*/
mbfa1 NVARCHAR(128) NOT NULL, /*FA分析*/
short_cut NVARCHAR(128) NOT NULL, /*短路电压*/
bgatype NVARCHAR(128), /*bga类型*/
BGAPN NVARCHAR(128), /*BGAPN*/
BGA_place NVARCHAR(128), /*BGA位置*/
bga_brief NVARCHAR(128) , /*BGA简述*/
repairer NVARCHAR(128) NOT NULL, /*bga维修人*/
repair_date date, /*bga维修日期*/
countNum NVARCHAR(128) NOT NULL/*总共录入了几次，从1开始，防止同一片板子在维修与BGA来回折腾*/
)

/*outlookcheck*/
CREATE TABLE outlookcheck(
Id INT PRIMARY KEY IDENTITY, 
track_serial_no NVARCHAR(128) NOT NULL,
tester NVARCHAR(128) NOT NULL,
test_date date
)

/*testall, TBG,DT, AIO*/
CREATE TABLE testalltable(
Id INT PRIMARY KEY IDENTITY, 
track_serial_no NVARCHAR(128) NOT NULL,
tester NVARCHAR(128) NOT NULL,
test_date date
)

/*Obe*/
CREATE TABLE Obetable(
Id INT PRIMARY KEY IDENTITY, 
track_serial_no NVARCHAR(128) NOT NULL,
tester NVARCHAR(128) NOT NULL,
test_date date
)

/*Running*/
CREATE TABLE Runningtable(
Id INT PRIMARY KEY IDENTITY, 
track_serial_no NVARCHAR(128) NOT NULL,
tester NVARCHAR(128) NOT NULL,
test_date date
)

/*test2 LBG 使用*/
CREATE TABLE test2table(
Id INT PRIMARY KEY IDENTITY, 
track_serial_no NVARCHAR(128) NOT NULL,
tester NVARCHAR(128) NOT NULL,
test_date date
)

/*test1 LBG*/
CREATE TABLE test1table(
Id INT PRIMARY KEY IDENTITY, 
track_serial_no NVARCHAR(128) NOT NULL,
tester NVARCHAR(128) NOT NULL,
test_date date
)

CREATE TABLE bga_repair_record_table(
Id INT PRIMARY KEY IDENTITY, 
track_serial_no NVARCHAR(128) NOT NULL, /*跟踪条码*/
vendor NVARCHAR(128) NOT NULL, /*厂商*/
product NVARCHAR(128) NOT NULL, /*客户别*/
source NVARCHAR(128) NOT NULL, /*来源*/
orderno NVARCHAR(128) NOT NULL, /*订单编号*/
receivedate date, /*收货日期*/
mb_brief NVARCHAR(128), /*MB简称*/
custom_serial_no NVARCHAR(128) NOT NULL, /*客户序号*/
vendor_serail_no NVARCHAR(128) NOT NULL, /*厂商序号*/
mpn NVARCHAR(128) NOT NULL, /*MPN*/
mb_make_date date, /*MB生产日期*/
customFault NVARCHAR(128) NOT NULL, /*客户故障*/
fault_describe NVARCHAR(128) NOT NULL, /*故障原因*/
mbfa1 NVARCHAR(128) NOT NULL, /*mbfa1*/
short_cut NVARCHAR(128) NOT NULL, /*短路电压*/
bgatype NVARCHAR(128), /*bga类型*/
BGAPN NVARCHAR(128), /*BGAPN*/
BGA_place NVARCHAR(128), /*BGA位置*/
bga_brief NVARCHAR(128) , /*BGA简述*/
repairer NVARCHAR(128) NOT NULL, /*维修人*/
repair_date date, /*修复日期*/

bga_repairer NVARCHAR(128) NOT NULL, /*bga维修人*/
bga_repair_date date, /*bga修复日期*/
bga_repair_result NVARCHAR(128) NOT NULL,/*bga修复状态*/
countNum NVARCHAR(128) NOT NULL,/*总共录入了几次，从1开始，防止同一片板子在维修与BGA来回折腾*/
oldSn NVARCHAR(128), /*换下sn*/
newSn NVARCHAR(128), /*换上sn*/
)

/*DPK 导入表格*/
CREATE TABLE DPK_table(
Id INT PRIMARY KEY IDENTITY, 
dpk_order_no NVARCHAR(128) NOT NULL, /*联想DPK订单编号*/
dpk_type NVARCHAR(128) NOT NULL, /*DPK类别*/
KEYPN NVARCHAR(128) NOT NULL, /*KEYPN*/
KEYID  NVARCHAR(128) NOT NULL, /*KEYID*/
KEYSERIAL NVARCHAR(128) NOT NULL, /*KEYSERIAL*/
upload_date date, /*上传日期*/
_status NVARCHAR(128) NOT NULL, /*状态*/
burn_date date,/*烧录日期*/
custom_serial_no NVARCHAR(128) /*客户序号*/
)

/*材料名称*/
CREATE TABLE materialNameTable(
Id INT PRIMARY KEY IDENTITY, 
materialName NVARCHAR(128) NOT NULL
)

/*维修故障类别*/
CREATE TABLE repairFaultType(
Id INT PRIMARY KEY IDENTITY, 
_type NVARCHAR(128) NOT NULL
)

CREATE TABLE repair_record_table(
Id INT PRIMARY KEY IDENTITY, 
track_serial_no NVARCHAR(128) NOT NULL, /*跟踪条码*/
vendor NVARCHAR(128) NOT NULL, /*厂商*/
product NVARCHAR(128) NOT NULL, /*客户别*/
source NVARCHAR(128) NOT NULL, /*来源*/
orderno NVARCHAR(128) NOT NULL, /*订单编号*/
receivedate date, /*收货日期*/
mb_describe NVARCHAR(128), /*MB描述*/
mb_brief NVARCHAR(128), /*MB简称*/
custom_serial_no NVARCHAR(128) NOT NULL, /*客户序号*/
vendor_serail_no NVARCHAR(128) NOT NULL, /*厂商序号*/
mpn NVARCHAR(128) NOT NULL, /*MPN*/
mb_make_date date, /*MB生产日期*/
customFault NVARCHAR(128) NOT NULL, /*客户故障*/
fault_describe NVARCHAR(128) NOT NULL, /*故障原因*/
mbfa1 NVARCHAR(128) NOT NULL, /*FA分析*/
short_cut NVARCHAR(128) NOT NULL, /*短路电压*/
software_update NVARCHAR(128) NOT NULL, /*软体更新*/
not_good_place NVARCHAR(128), /*不良位置*/
material_mpn NVARCHAR(128), /*材料MPN*/
material_71pn NVARCHAR(128), /*材料71PN*/
material_type NVARCHAR(128), /*材料类别*/
fault_type NVARCHAR(128), /*故障类别*/
_action NVARCHAR(128) NOT NULL, /*动作*/
ECO NVARCHAR(128), /*ECO*/
repair_result NVARCHAR(128) NOT NULL, /*修复结果*/
repairer NVARCHAR(128) NOT NULL, /*维修人*/
repair_date date, /*修复日期*/
)

CREATE TABLE LCFC71BOM_table(
Id INT PRIMARY KEY IDENTITY, 
_date date, /*日期*/
mb_brief NVARCHAR(128) NOT NULL, /*MB简称*/
material_vendor_pn NVARCHAR(128), /*材料厂商PN*/
material_mpn NVARCHAR(128), /*材料MPN*/
_description NVARCHAR(128), /*Description*/
price NVARCHAR(128)/*price*/
)


CREATE TABLE LCFC_MBBOM_table(
Id INT PRIMARY KEY IDENTITY, 
_date date, /*日期*/
vendor NVARCHAR(128) NOT NULL, /*厂商*/
product NVARCHAR(128) NOT NULL, /*客户别*/
mb_brief NVARCHAR(128) NOT NULL, /*MB简称*/
MPN NVARCHAR(128), /*材料MPN*/
material_mpn NVARCHAR(128), /*材料MPN*/
material_box_place NVARCHAR(128), /*料盒位置*/
material_describe NVARCHAR(128), /*物料描述*/
material_num NVARCHAR(128), /*用料数量*/
L1 NVARCHAR(128) , /*L1*/
L2 NVARCHAR(128) , /*L2*/
L3 NVARCHAR(128) , /*L3*/
L4 NVARCHAR(128) , /*L4*/
L5 NVARCHAR(128) , /*L5*/
L6 NVARCHAR(128) , /*L6*/
L7 NVARCHAR(128) , /*L7*/
L8 NVARCHAR(128)  /*L8*/ 
)

/*客责类别*/
CREATE TABLE customResponsibilityType(
Id INT PRIMARY KEY IDENTITY, 
_type NVARCHAR(128) NOT NULL
)


/*还货状态*/
CREATE TABLE returnStoreStatus(
Id INT PRIMARY KEY IDENTITY, 
_status NVARCHAR(128) NOT NULL
)

/*还货表*/
CREATE TABLE returnStore(
Id INT PRIMARY KEY IDENTITY, 
vendor NVARCHAR(128) NOT NULL, /*--厂商*/
product NVARCHAR(128) NOT NULL, /*--客户别*/
return_file_no NVARCHAR(128) NOT NULL,/*--还货文件编号*/
storehouse NVARCHAR(128) NOT NULL,/*--客户库别*/
return_date date,/*--还货时间*/
orderno NVARCHAR(128),/*--订单编号*/
custommaterialNo NVARCHAR(128) NOT NULL,/*--客户料号*/
dpkpn NVARCHAR(128),/*--DPK状态*/
track_serial_no NVARCHAR(128),/*--跟踪条码*/
custom_serial_no NVARCHAR(128),/*--客户序号*/
vendor_serail_no NVARCHAR(128) NOT NULL,/*--厂商序号*/
vendormaterialNo NVARCHAR(128),/*--厂商料号*/
_status NVARCHAR(128),/*--状态*/
custom_res_type NVARCHAR(128),/*--客责类别*/
response_describe NVARCHAR(128),/*--客责描述*/
tat NVARCHAR(128),/*--TAT*/
inputuser NVARCHAR(128),/*还货人*/
lenovo_maintenance_no NVARCHAR(128),/*联想维修站编号*/
lenovo_repair_no NVARCHAR(128),/*联想维修单编号*/
returnOrderIndex NVARCHAR(128)/*还货序列号，订单号+料号+顺序*/
)


/*收货*/
CREATE TABLE DeliveredTable(
Id INT PRIMARY KEY IDENTITY, 
vendor NVARCHAR(128) NOT NULL, /*厂商*/
product NVARCHAR(128) NOT NULL, /*客户别*/
source_brief NVARCHAR(128) NOT NULL,/*来源*/
storehouse NVARCHAR(128) NOT NULL,/*库别*/
custom_order NVARCHAR(128) NOT NULL,/*订单编号*/
order_out_date date,/*客户出库日期*/
order_receive_date date,/*收货日期*/
custom_machine_type NVARCHAR(128),/*客户机型*/
mb_brief NVARCHAR(128) NOT NULL,/*mb简称*/
custommaterialNo NVARCHAR(128) NOT NULL,/*客户料号*/
dpk_status NVARCHAR(128) NOT NULL,/*DPK状态*/
track_serial_no NVARCHAR(128) NOT NULL,/*跟踪条码*/
custom_serial_no NVARCHAR(128) NOT NULL,/*客户序号*/
vendor_serail_no NVARCHAR(128) NOT NULL,/*厂商序号*/
uuid NVARCHAR(128) NOT NULL,/*UUID*/
mac NVARCHAR(128) NOT NULL,/*MAC*/
mpn NVARCHAR(128) NOT NULL,/*厂商料号*/
mb_describe NVARCHAR(128) NOT NULL,/*mb描述*/
mb_make_date date,/*MB生产日期*/
warranty_period NVARCHAR(128) NOT NULL,/*保修期*/
custom_fault NVARCHAR(128) NOT NULL,/*客户故障*/
guarantee NVARCHAR(128) NOT NULL,/*保内/保外*/
customResponsibility NVARCHAR(128) NOT NULL,/*客责描述*/
lenovo_custom_service_no NVARCHAR(128),/*联想客服序号*/
lenovo_maintenance_no NVARCHAR(128),/*联想维修站编号*/
lenovo_repair_no NVARCHAR(128),/*联想维修单编号*/
whole_machine_no NVARCHAR(128),/*整机序号*/
inputuser NVARCHAR(128),/*收货人*/
receiveOrderIndex NVARCHAR(128)/*收货序列号，订单号+料号+顺序*/
)

/*MB状态转换表*/
CREATE TABLE DeliveredTableTransfer(
Id INT PRIMARY KEY IDENTITY, 
vendor NVARCHAR(128) NOT NULL, /*厂商*/
product NVARCHAR(128) NOT NULL, /*客户别*/
source_brief NVARCHAR(128) NOT NULL,/*来源*/
storehouse NVARCHAR(128) NOT NULL,/*库别*/
custom_order NVARCHAR(128) NOT NULL,/*订单编号*/
order_out_date date,/*客户出库日期*/
order_receive_date date,/*收货日期*/
custom_machine_type NVARCHAR(128),/*客户机型*/
mb_brief NVARCHAR(128) NOT NULL,/*mb简称*/
custommaterialNo NVARCHAR(128) NOT NULL,/*客户料号*/
dpk_status NVARCHAR(128) NOT NULL,/*DPK状态*/
track_serial_no NVARCHAR(128) NOT NULL,/*跟踪条码*/
custom_serial_no NVARCHAR(128) NOT NULL,/*客户序号*/
vendor_serail_no NVARCHAR(128) NOT NULL,/*厂商序号*/
uuid NVARCHAR(128) NOT NULL,/*UUID*/
mac NVARCHAR(128) NOT NULL,/*MAC*/
mpn NVARCHAR(128) NOT NULL,/*厂商料号*/
mb_describe NVARCHAR(128) NOT NULL,/*mb描述*/
mb_make_date date,/*MB生产日期*/
warranty_period NVARCHAR(128) NOT NULL,/*保修期*/
custom_fault NVARCHAR(128) NOT NULL,/*客户故障*/
guarantee NVARCHAR(128) NOT NULL,/*保内/保外*/
customResponsibility NVARCHAR(128) NOT NULL,/*客责描述*/
lenovo_custom_service_no NVARCHAR(128),/*联想客服序号*/
lenovo_maintenance_no NVARCHAR(128),/*联想维修站编号*/
lenovo_repair_no NVARCHAR(128),/*联想维修单编号*/
whole_machine_no NVARCHAR(128),/*整机序号*/
inputuser NVARCHAR(128),/*收货人*/
receiveOrderIndex NVARCHAR(128),/*收货序列号，订单号+料号+顺序*/
track_serial_no_transfer NVARCHAR(128) NOT NULL,/*新的跟踪条码*/
modifier NVARCHAR(128),/*修改人*/
modify_date date,/*MB修改日期*/
)

CREATE TABLE fruDeliveredTable(
Id INT PRIMARY KEY IDENTITY, 
orderno NVARCHAR(128) NOT NULL,/*订单编号*/
vendor NVARCHAR(128) NOT NULL, /*厂商*/
product NVARCHAR(128) NOT NULL, /*客户别*/
customermaterialno NVARCHAR(128) NOT NULL,/*客户料号*/
machine_type NVARCHAR(128) NOT NULL,/*机型*/
name NVARCHAR(128) NOT NULL, /*名称*/
customermaterialdes NVARCHAR(1280) NOT NULL,/*客户物料描述*/
peijian_no NVARCHAR(128) NOT NULL,/*配件序号*/
customer_serial_no NVARCHAR(128) NOT NULL,/*客户序号*/
custom_fault NVARCHAR(128) NOT NULL,/*客户故障*/
make_date date, /*生产日期*/
gurantee NVARCHAR(128) NOT NULL,/*保内/保外*/
gurantee_note NVARCHAR(128) NOT NULL,/*保外备注*/
vendor_material_no NVARCHAR(128) NOT NULL,/*厂商料号*/
mpn1 NVARCHAR(128) NOT NULL,/*MPN1*/
receiver NVARCHAR(128) NOT NULL,/*收件人*/
receive_date date,/*收货日期*/
lenovo_maintenance_no NVARCHAR(128) /*后加字段，维修站编号 */
)

CREATE TABLE frureturnStore(
Id INT PRIMARY KEY IDENTITY, 
orderno NVARCHAR(128) NOT NULL,/*订单编号*/
vendor NVARCHAR(128) NOT NULL, /*厂商*/
product NVARCHAR(128) NOT NULL, /*客户别*/
customermaterialno NVARCHAR(128) NOT NULL,/*客户料号*/
machine_type NVARCHAR(128) NOT NULL,/*机型*/
name NVARCHAR(128) NOT NULL, /*名称*/
customermaterialdes NVARCHAR(1280) NOT NULL,/*客户物料描述*/
peijian_no NVARCHAR(128) NOT NULL,/*配件序号*/
customer_serial_no NVARCHAR(128) NOT NULL,/*客户序号*/
custom_fault NVARCHAR(128) NOT NULL,/*客户故障*/
make_date date, /*生产日期*/
gurantee NVARCHAR(128) NOT NULL,/*保内/保外*/
gurantee_note NVARCHAR(128) NOT NULL,/*保外备注*/
vendor_material_no NVARCHAR(128) NOT NULL,/*厂商料号*/
mpn1 NVARCHAR(128) NOT NULL,/*MPN1*/
receiver NVARCHAR(128) NOT NULL,/*还货人*/
receive_date date,/*还货日期*/
tat NVARCHAR(128) NOT NULL,/*时间差*/
_status NVARCHAR(128) NOT NULL,/*状态，良品不良品*/
)


/*仓库别 来货*/
CREATE TABLE storehouse(
Id INT PRIMARY KEY IDENTITY, 
storehouse_describe NVARCHAR(128) NOT NULL
)

/*客责描述*/
CREATE TABLE customResponsibility(
Id INT PRIMARY KEY IDENTITY, 
responsibility_describe NVARCHAR(128) NOT NULL
)

/*保内/保外*/
CREATE TABLE guarantee(
Id INT PRIMARY KEY IDENTITY, 
guarantee_describe NVARCHAR(128) NOT NULL
)

/*客户故障*/
CREATE TABLE customFault(
Id INT PRIMARY KEY IDENTITY, 
fault_index NVARCHAR(128) NOT NULL,
fault_describe NVARCHAR(128) NOT NULL
)

/*来源	正常RMA\RMA_DOA\NB_DOA\RMA_IQC\成都DT\北京DT\惠阳DT*/
CREATE TABLE sourceTable(
Id INT PRIMARY KEY IDENTITY, 
source NVARCHAR(128) NOT NULL
)

/*users表*/
CREATE TABLE users(
Id INT PRIMARY KEY IDENTITY, 
username NVARCHAR(128) NOT NULL, 
workId NVARCHAR(128) NOT NULL, 
_password NVARCHAR(128) NOT NULL, 
super_manager NVARCHAR(128) NOT NULL,
bga  NVARCHAR(128) NOT NULL,
repair  NVARCHAR(128) NOT NULL,
test_all  NVARCHAR(128) NOT NULL,
test1  NVARCHAR(128) NOT NULL,
test2  NVARCHAR(128) NOT NULL,
receive_return  NVARCHAR(128) NOT NULL,
store  NVARCHAR(128) NOT NULL,
outlook  NVARCHAR(128) NOT NULL,
running  NVARCHAR(128) NOT NULL,
obe  NVARCHAR(128) NOT NULL,
)

/*客户别*/
CREATE TABLE vendorProduct(Id INT PRIMARY KEY IDENTITY, vendor NVARCHAR(128) NOT NULL, product NVARCHAR(128) NOT NULL)

/*
INSERT INTO users VALUES('Value1','Value1','Value1')

/*修改列名：*/
sp_rename 'DeliveredTable.vendormaterialNo',mpn,'column'

/*表中添加列*/
alter table MBMaterialCompare add inputuser NVARCHAR(128)

/*倒叙查询最新的数据：*/
select top 9 * from receiveOrder order by id desc*/

/*MB物料对照表*/
CREATE TABLE MBMaterialCompare(
Id INT PRIMARY KEY IDENTITY, 
vendor NVARCHAR(128) NOT NULL, /*厂商*/
product NVARCHAR(128) NOT NULL, /*客户别*/
mb_brief NVARCHAR(128) NOT NULL,/*MB简称*/
vendormaterialNo NVARCHAR(128) NOT NULL,/*厂商料号*/
mpn NVARCHAR(128) NOT NULL,/*MPN*/
replace_mpn NVARCHAR(128),/*可替换MPN*/
custommaterialNo NVARCHAR(128) NOT NULL,/*客户料号*/
replace_custom_materialNo NVARCHAR(128),/*可替换客户料号*/
fruNo NVARCHAR(128),/*FRU料号*/
replace_fruNo NVARCHAR(128),/*可替换FRU料号*/
mb_descripe NVARCHAR(128) NOT NULL,/*MB描述*/
vendor_pch_mpn NVARCHAR(128),/*厂商PCH_MPN*/
pcb_brief_describe NVARCHAR(128),/*PCH简述*/
pcb_describe NVARCHAR(128),/*PCH描述*/
vendor_vga_mpn NVARCHAR(128),/*厂商VGA_MPN*/
vga_brief_describe NVARCHAR(128),/*VGA简述*/
vga_describe NVARCHAR(128),/*VGA描述*/
vendor_cpu_mpn NVARCHAR(128),/*厂商CPU_MPN*/
cpu_brief NVARCHAR(128),/*CPU简述*/
cpu_describe NVARCHAR(128),/*CPU描述*/
dpk_type NVARCHAR(128),/*DPK类型*/
dpkpn NVARCHAR(128),/*DPKPN*/
warranty_period NVARCHAR(128) NOT NULL,/*保修期*/
custom_machine_type NVARCHAR(128),/*客户机型*/
whole_machine_num NVARCHAR(128) NOT NULL,/*整机出货量*/
area NVARCHAR(128) NOT NULL,/*地区*/
_status NVARCHAR(128) NOT NULL,/*状态*/
cpu_type NVARCHAR(128),/*CPU型号*/
cpu_freq NVARCHAR(128),/*CPU频率*/
eco NVARCHAR(128),/*ECO*/
eol NVARCHAR(128),/*材料采购类别*/
adddate date,/*添加日期*/
inputuser NVARCHAR(128),/*添加人*/
)

/*cid描述*/
CREATE TABLE cidRecord(
Id INT PRIMARY KEY IDENTITY,
track_serial_no NVARCHAR(128) NOT NULL,/*跟踪条码*/
vendor NVARCHAR(128) NOT NULL, /*厂商*/
product NVARCHAR(128) NOT NULL, /*客户别*/
custom_order NVARCHAR(128) NOT NULL,/*订单编号*/
custommaterialNo NVARCHAR(128) NOT NULL,/*客户料号*/
custom_serial_no NVARCHAR(128) NOT NULL,/*客户序号*/
mb_brief NVARCHAR(128) NOT NULL,/*mb简称*/
mpn NVARCHAR(128) NOT NULL,/*厂商料号*/
order_receive_date date,/*收货日期*/
custom_fault NVARCHAR(128) NOT NULL,/*客户故障*/
custom_res_type NVARCHAR(128),/*--客责类别*/
customResponsibility NVARCHAR(128) NOT NULL,/*客责描述*/
short_cut NVARCHAR(128) NOT NULL, /*短路电压*/
inputuser NVARCHAR(128),/*录入人*/
inputdate date/*录入日期*/
)

/*收货单*/
CREATE TABLE receiveOrder(
Id INT PRIMARY KEY IDENTITY,
vendor NVARCHAR(128) NOT NULL, /*厂商*/
product NVARCHAR(128) NOT NULL, /*客户别*/
orderno NVARCHAR(128) NOT NULL,/*订单编号*/
custom_materialNo NVARCHAR(128) NOT NULL,/*客户料号*/
custom_material_describe NVARCHAR(128) NOT NULL,/*客户物料描述*/
ordernum NVARCHAR(128) NOT NULL,/*订单数量*/
mb_brief NVARCHAR(128) NOT NULL,/*MB简称*/
vendor_materialNo NVARCHAR(128) NOT NULL,/*厂商料号*/
username NVARCHAR(128) NOT NULL,/*制单人*/
ordertime date,/*制单时间*/
receivedNum NVARCHAR(128),/*收货数量*/
receivedate date,/*收货日期*/
_status NVARCHAR(128) NOT NULL,/*订单状态*/
storehouse NVARCHAR(128) NOT NULL,/*库别*/
returnNum NVARCHAR(128), /*还货数量*/

cid_number NVARCHAR(128), /*废弃数量*/
)

/*fru收货单*/
CREATE TABLE frureceiveOrder(
Id INT PRIMARY KEY IDENTITY,
vendor NVARCHAR(128) NOT NULL, /*厂商*/
product NVARCHAR(128) NOT NULL, /*客户别*/
orderno NVARCHAR(128) NOT NULL,/*订单编号*/
custom_materialNo NVARCHAR(128) NOT NULL,/*客户料号*/
custom_material_describe NVARCHAR(128) NOT NULL,/*客户物料描述*/
ordernum NVARCHAR(128) NOT NULL,/*订单数量*/
mb_brief NVARCHAR(128) NOT NULL,/*MB简称 ->机型*/
vendor_materialNo NVARCHAR(128) NOT NULL,/*厂商料号 ->MPN1*/
username NVARCHAR(128) NOT NULL,/*制单人*/
ordertime date,/*制单时间*/
receivedNum NVARCHAR(128),/*收货数量*/
receivedate date,/*收货日期*/
_status NVARCHAR(128) NOT NULL,/*订单状态*/
storehouse NVARCHAR(128) NOT NULL,/*库别*/
returnNum NVARCHAR(128), /*还货数量*/

cid_number NVARCHAR(128), /*废弃数量*/
)

/*站别记录信息*/
CREATE TABLE stationInformation(
Id INT PRIMARY KEY IDENTITY, 
track_serial_no NVARCHAR(128) NOT NULL, /*跟踪条码*/
station NVARCHAR(128), /*站别信息：收货，维修，(BGA), 测试1， 测试2(或测试1&2）， 外观等，[合肥有Test2->Running->外观->OBE]*/
updateDate date /*更新时间*/
)

/*合肥海关信息*/
CREATE TABLE company_fixed_table(
Id INT PRIMARY KEY IDENTITY, 
indentifier NVARCHAR(128) NOT NULL, /*企业编号*/
book_number NVARCHAR(128), /*账册编号*/
electronic_number NVARCHAR(128), /*电子账册号*/
)

/*SELECT NAME FROM SYSOBJECTS WHERE TYPE='U'SELECT * FROM INFORMATION_SCHEMA.TABLES  列出所有表格*/