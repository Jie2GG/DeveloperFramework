using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeveloperFramework.Utility
{
	/// <summary>
	/// 伪随机数使用工具
	/// </summary>
	public static class RandomUtility
	{
		#region --字段--
		private static readonly Random _random = new Random (Guid.NewGuid ().GetHashCode ());
		private static readonly string[] _surname = { "赵", "钱", "孙", "李", "周", "吴", "郑", "王", "冯", "陈", "褚", "卫", "蒋", "沈", "韩", "杨", "朱", "秦", "尤", "许", "何", "吕", "施", "张", "孔", "曹", "严", "华", "金", "魏", "陶", "姜", "戚", "谢", "邹", "喻", "柏", "水", "窦", "章", "云", "苏", "潘", "葛", "奚", "范", "彭", "郎", "鲁", "韦", "昌", "马", "苗", "凤", "花", "方", "任", "袁", "柳", "鲍", "史", "唐", "费", "薛", "雷", "贺", "倪", "汤", "滕", "殷", "罗", "毕", "郝", "安", "常", "傅", "卞", "齐", "元", "顾", "孟", "平", "黄", "穆", "萧", "尹", "姚", "邵", "湛", "汪", "祁", "毛", "狄", "米", "伏", "成", "戴", "谈", "宋", "茅", "庞", "熊", "纪", "舒", "屈", "项", "祝", "董", "梁", "杜", "阮", "蓝", "闵", "季", "贾", "路", "娄", "江", "童", "颜", "郭", "梅", "盛", "林", "钟", "徐", "邱", "骆", "高", "夏", "蔡", "田", "樊", "胡", "凌", "霍", "虞", "万", "支", "柯", "管", "卢", "莫", "柯", "房", "裘", "缪", "解", "应", "宗", "丁", "宣", "邓", "单", "杭", "洪", "包", "诸", "左", "石", "崔", "吉", "龚", "程", "嵇", "邢", "裴", "陆", "荣", "翁", "荀", "于", "惠", "甄", "曲", "封", "储", "仲", "伊", "宁", "仇", "甘", "武", "符", "刘", "景", "詹", "龙", "叶", "幸", "司", "黎", "溥", "印", "怀", "蒲", "邰", "从", "索", "赖", "卓", "屠", "池", "乔", "胥", "闻", "莘", "党", "翟", "谭", "贡", "劳", "逄", "姬", "申", "扶", "堵", "冉", "宰", "雍", "桑", "寿", "通", "燕", "浦", "尚", "农", "温", "别", "庄", "晏", "柴", "瞿", "阎", "连", "习", "容", "向", "古", "易", "廖", "庾", "终", "步", "都", "耿", "满", "弘", "匡", "国", "文", "寇", "广", "禄", "阙", "东", "欧", "利", "师", "巩", "聂", "关", "荆", "司马", "上官", "欧阳", "夏侯", "诸葛", "闻人", "东方", "赫连", "皇甫", "尉迟", "公羊", "澹台", "公冶", "宗政", "濮阳", "淳于", "单于", "太叔", "申屠", "公孙", "仲孙", "轩辕", "令狐", "徐离", "宇文", "长孙", "慕容", "司徒", "司空", "皮" };
		private static readonly string[] _name = { "伟", "刚", "勇", "毅", "俊", "峰", "强", "军", "平", "保", "东", "文", "辉", "力", "明", "永", "健", "世", "广", "志", "义", "兴", "良", "海", "山", "仁", "波", "宁", "贵", "福", "生", "龙", "元", "全", "国", "胜", "学", "祥", "才", "发", "武", "新", "利", "清", "飞", "彬", "富", "顺", "信", "子", "杰", "涛", "昌", "成", "康", "星", "光", "天", "达", "安", "岩", "中", "茂", "进", "林", "有", "坚", "和", "彪", "博", "诚", "先", "敬", "震", "振", "壮", "会", "思", "群", "豪", "心", "邦", "承", "乐", "绍", "功", "松", "善", "厚", "庆", "磊", "民", "友", "裕", "河", "哲", "江", "超", "浩", "亮", "政", "谦", "亨", "奇", "固", "之", "轮", "翰", "朗", "伯", "宏", "言", "若", "鸣", "朋", "斌", "梁", "栋", "维", "启", "克", "伦", "翔", "旭", "鹏", "泽", "晨", "辰", "士", "以", "建", "家", "致", "树", "炎", "德", "行", "时", "泰", "盛", "雄", "琛", "钧", "冠", "策", "腾", "楠", "榕", "风", "航", "弘", "秀", "娟", "英", "华", "慧", "巧", "美", "娜", "静", "淑", "惠", "珠", "翠", "雅", "芝", "玉", "萍", "红", "娥", "玲", "芬", "芳", "燕", "彩", "春", "菊", "兰", "凤", "洁", "梅", "琳", "素", "云", "莲", "真", "环", "雪", "荣", "爱", "妹", "霞", "香", "月", "莺", "媛", "艳", "瑞", "凡", "佳", "嘉", "琼", "勤", "珍", "贞", "莉", "桂", "娣", "叶", "璧", "璐", "娅", "琦", "晶", "妍", "茜", "秋", "珊", "莎", "锦", "黛", "青", "倩", "婷", "姣", "婉", "娴", "瑾", "颖", "露", "瑶", "怡", "婵", "雁", "蓓", "纨", "仪", "荷", "丹", "蓉", "眉", "君", "琴", "蕊", "薇", "菁", "梦", "岚", "苑", "婕", "馨", "瑗", "琰", "韵", "融", "园", "艺", "咏", "卿", "聪", "澜", "纯", "毓", "悦", "昭", "冰", "爽", "琬", "茗", "羽", "希", "欣", "飘", "育", "滢", "馥", "筠", "柔", "竹", "霭", "凝", "晓", "欢", "霄", "枫", "芸", "菲", "寒", "伊", "亚", "宜", "可", "姬", "舒", "影", "荔", "枝", "丽", "阳", "妮", "宝", "贝", "初", "程", "梵", "罡", "恒", "鸿", "桦", "骅", "剑", "娇", "纪", "宽", "苛", "灵", "玛", "媚", "琪", "晴", "容", "睿", "烁", "堂", "唯", "威", "韦", "雯", "苇", "萱", "阅", "彦", "宇", "雨", "洋", "忠", "宗", "曼", "紫", "逸", "贤", "蝶", "菡", "绿", "蓝", "儿", "翠", "烟" };
		private static readonly Dictionary<string, Dictionary<string, string[]>> _areas = new Dictionary<string, Dictionary<string, string[]>> ()
		{
			{"北京市",  new Dictionary<string,  string[]> () {
				{ "北京城区",  new string[] { "东城区",  "西城区",  "朝阳区",  "丰台区",  "石景山区",  "海淀区",  "门头沟区",  "房山区",  "通州区",  "顺义区",  "昌平区",  "大兴区",  "怀柔区",  "平谷区",  "密云区",  "延庆区" }}
			}},
			{ "天津市",  new Dictionary<string,  string[]>() {
				{ "天津城区",  new string[] { "和平区",  "河东区",  "河西区",  "南开区",  "河北区",  "红桥区",  "东丽区",  "西青区",  "津南区",  "北辰区",  "武清区",  "宝坻区",  "滨海新区", "宁河区", "静海区", "蓟州区" }}
			}},
			{ "河北省",  new Dictionary<string,  string[]>() {
				{ "石家庄市",  new string[] { "长安区", "桥西区", "新华区", "井陉矿区", "裕华区", "藁城区", "鹿泉区", "栾城区", "井陉县", "正定县", "行唐县", "灵寿县", "高邑县", "深泽县", "赞皇县", "无极县", "平山县", "元氏", "赵县", "辛集市", "晋州市", "新乐市" }},
				{ "唐山市",  new string[] { "路南区", "路北区", "古冶区", "开平区", "丰南区", "丰润区", "曹妃甸区", "滦南县", "亭县", "迁西县", "玉田县", "遵化市", "迁安市", "滦州市" }},
				{ "秦皇岛市",  new string[] { "海港区", "山海关区", "北戴河区", "抚宁区", "青龙满族自治县", "昌黎县", "卢龙县"}},
				{ "邯郸市",  new string[] { "邯山区", "丛台区", "复兴区", "峰峰矿区", "肥乡区", "永年区", "临漳县", "成安县", "大名县", "涉县", "磁县", "邱县", "鸡泽县", "广平县", "馆陶县", "魏县", "曲周县", "武安市" }},
				{ "邢台市",  new string[] { "桥东区", "桥西区", "邢台县", "临城县", "内丘县", "柏乡县", "隆尧县", "任县", "南和县", "宁晋县", "巨鹿县", "新河县", "广宗县", "平乡县", "威县", "清河县", "临西县", "南宫市", "沙河市" }},
				{ "保定市",  new string[] { "竞秀区", "莲池区", "满城区", "清苑区", "徐水区", "涞水县", "阜平县", "定兴县", "唐县", "高阳县", "容城县", "涞源县", "望都县", "安新县", "易县", "曲阳县", "蠡县", "顺平县", "博野县", "雄县", "涿州市", "定州市", "安国市", "高碑店市" }},
				{ "张家口市",  new string[] { "桥东区", "桥西区", "宣化区", "下花园区", "万全区", "崇礼区", "张北县", "康保县", "沽源县", "尚义县", "蔚县", "阳原县", "怀安县", "怀来县", "涿鹿县", "赤城县" }},
				{ "承德市",  new string[] { "双桥区", "双滦区", "鹰手营子矿区", "承德县", "兴隆县", "滦平县", "隆化县", "丰宁满族自治县", "宽城满族自治县", "围场满族蒙古族自治县", "平泉市" }},
				{ "沧州市",  new string[] { "新华区", "运河区", "沧县", "青县", "东光县", "海兴县", "盐山县", "肃宁县", "南皮县", "吴桥县", "献县", "孟村回族自治县", "泊头市", "任丘市", "黄骅市", "河间市" }},
				{ "廊坊市",  new string[] { "安次区", "广阳区", "固安县", "永清县", "香河县", "大城县", "文安县", "大厂回族自治县", "霸州市", "三河市" }},
				{ "衡水市",  new string[] { "桃城区", "冀州区", "枣强县", "武邑县", "武强县", "饶阳县", "安平县", "故城县", "景县", "阜城县", "深州市" }}
			}},
			{ "山西省",  new Dictionary<string,  string[]>() {
				{ "太原市",  new string[] { }},
				{ "大同市",  new string[] { }},
				{ "阳泉市",  new string[] { }},
				{ "长治市",  new string[] { }},
				{ "晋城市",  new string[] { }},
				{ "朔州市",  new string[] { }},
				{ "晋中市",  new string[] { }},
				{ "运城市",  new string[] { }},
				{ "忻州市",  new string[] { }},
				{ "临汾市",  new string[] { }},
				{ "吕梁市",  new string[] { }},
			}},
			{ "内蒙古自治区",  new Dictionary<string,  string[]>() {

			}},
			{ "辽宁省",  new Dictionary<string,  string[]>() {

			}},
			{ "吉林省",  new Dictionary<string,  string[]>() {

			}},
			{ "黑龙江省",  new Dictionary<string,  string[]>() {

			}},
			{ "上海市",  new Dictionary<string,  string[]>() {

			}},
			{ "江苏省",  new Dictionary<string,  string[]>() {

			}},
			{ "浙江省",  new Dictionary<string,  string[]>() {

			}},
			{ "安徽省",  new Dictionary<string,  string[]>() {

			}},
			{ "福建省",  new Dictionary<string,  string[]>() {

			}},
			{ "江西省",  new Dictionary<string,  string[]>() {

			}},
			{ "山东省",  new Dictionary<string,  string[]>() {

			}},
			{ "河南省",  new Dictionary<string,  string[]>() {

			}},
			{ "湖北省",  new Dictionary<string,  string[]>() {

			}},
			{ "湖南省",  new Dictionary<string,  string[]>() {

			}},
			{ "广东省",  new Dictionary<string,  string[]>() {

			}},
			{ "广西壮族自治区",  new Dictionary<string,  string[]>() {

			}},
			{ "海南省",  new Dictionary<string,  string[]>() {

			}},
			{ "重庆市",  new Dictionary<string,  string[]>() {

			}},
			{ "四川省",  new Dictionary<string,  string[]>() {

			}},
			{ "贵州省",  new Dictionary<string,  string[]>() {

			}},
			{ "云南省",  new Dictionary<string,  string[]>() {

			}},
			{ "西藏自治区",  new Dictionary<string,  string[]>() {

			}},
			{ "陕西省",  new Dictionary<string,  string[]>() {

			}},
			{ "甘肃省",  new Dictionary<string,  string[]>() {

			}},
			{ "青海省",  new Dictionary<string,  string[]>() {

			}},
			{ "宁夏回族自治区",  new Dictionary<string,  string[]>() {

			}},
			{ "新疆维吾尔自治区",  new Dictionary<string,  string[]>() {

			}},
			{ "台湾省",  new Dictionary<string,  string[]>() {

			}},
			{ "香港特别行政区",  new Dictionary<string,  string[]>() {

			}},
			{ "澳门特别行政区",  new Dictionary<string,  string[]>() {

			}}
		};
		#endregion

		static RandomUtility ()
		{
			HttpClient http = new HttpClient ();
			http.DefaultRequestHeaders.Referrer = new Uri ("https://lbs.qq.com/service/webService/webServiceGuide/webServiceDistrict");
			Task<string> task = http.GetStringAsync (new Uri ("https://apis.map.qq.com/ws/district/v1/list?key=OB4BZ-D4W3U-B7VVO-4PJWW-6TKDJ-WPB77"));
		}

		/// <summary>
		/// 获取一个指定范围的随机 <see cref="int"/> 数
		/// </summary>
		/// <param name="minValue">随机数的下限值 (包含此值)</param>
		/// <param name="maxValue">随机数的上限值 (不包含此值)</param>
		/// <returns>返回随机的 <see cref="int"/> 值</returns>
		public static int RandomInt32 (int minValue, int maxValue)
		{
			return _random.Next (minValue, maxValue);
		}
		/// <summary>
		/// 获取一个指定随机的年龄,  其范围是 0 ~ 120
		/// </summary>
		/// <returns></returns>
		public static int RandomAge ()
		{
			return RandomInt32 (0, 120);
		}
		/// <summary>
		/// 获取一个随机的名字
		/// </summary>
		/// <returns>返回名字字符串</returns>
		public static string RandomName ()
		{
			int surnameIndex = RandomInt32 (0, _surname.Length);
			int nameIndex = RandomInt32 (0, _name.Length);
			return $"{_surname[surnameIndex]}{_name[nameIndex]}";
		}
		/// <summary>
		/// 获取一个随机的地区
		/// </summary>
		/// <returns></returns>
		public static string RendomArea ()
		{
			string provincias = _areas.Keys.ElementAt (RandomInt32 (0, _areas.Count));
			string municipal = _areas[provincias].Keys.ElementAt (RandomInt32 (0, _areas[provincias].Keys.Count));
			string county = _areas[provincias][municipal][RandomInt32 (0, _areas[provincias][municipal].Length)];

			return $"{provincias} {municipal} {county}";
		}
	}
}
