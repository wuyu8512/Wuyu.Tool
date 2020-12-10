using System;
using System.Text;
using System.Text.RegularExpressions;

namespace Tool.Expansion
{
	public static class ExpansionString
	{
		public static string Between(this string str, string leftStr, string rightStr, bool ignoreCase = false)
		{
			StringComparison comparisonType = StringComparison.CurrentCulture;
			if (ignoreCase)
			{
				comparisonType = StringComparison.OrdinalIgnoreCase;
			}
			int num = str.IndexOf(leftStr, comparisonType);
			if (num == -1)
			{
				return "";
			}
			int num2 = num + leftStr.Length;
			int num3 = str.IndexOf(rightStr, num2, comparisonType);
			if (num3 == -1)
			{
				return "";
			}
			return str.Substring(num2, num3 - num2);
		}

		public static string GetLeft(this string str, string left, bool ignoreCase = false)
		{
			StringComparison comparisonType = StringComparison.CurrentCulture;
			if (ignoreCase)
			{
				comparisonType = StringComparison.OrdinalIgnoreCase;
			}
			int num = str.IndexOf(left, comparisonType);
			if (num == -1)
			{
				return "";
			}
			return str.Substring(0, num);
		}

		public static string GetRight(this string str, string right, bool ignoreCase = false)
		{
			StringComparison comparisonType = StringComparison.CurrentCulture;
			if (ignoreCase)
			{
				comparisonType = StringComparison.OrdinalIgnoreCase;
			}
			int num = str.LastIndexOf(right, comparisonType);
			if (num == -1)
			{
				return "";
			}
			int num2 = num + right.Length;
			int length = str.Length - num2;
			return str.Substring(num2, length);
		}

		public static string RegMatch(string regStr, string text, int index)
		{
			Match match = new Regex(regStr).Match(text);
			if (!match.Success)
			{
				return string.Empty;
			}
			return match.Groups[index].Value;
		}

		public static int GetRandNumber(int min, int max)
		{
			Random random = new Random(GetRandomGuid());
			if (min > max)
			{
				int num = min;
				min = max;
				max = num;
			}
			max++;
			return random.Next(min, max);
		}

		public static string GetRandJs()
		{
			return new Random(GetRandomGuid()).NextDouble().ToString();
		}

		public static string GetRandstr(int count, int char_type = 0)
		{
			char[] array = new char[36]
			{
				'0',
				'1',
				'2',
				'3',
				'4',
				'5',
				'6',
				'7',
				'8',
				'9',
				'a',
				'b',
				'c',
				'd',
				'e',
				'f',
				'g',
				'h',
				'i',
				'j',
				'k',
				'l',
				'm',
				'n',
				'o',
				'p',
				'q',
				'r',
				's',
				't',
				'u',
				'v',
				'w',
				'x',
				'y',
				'z'
			};
			StringBuilder stringBuilder = new StringBuilder(62);
			Random random = new Random(GetRandomGuid());
			for (int i = 0; i < count; i++)
			{
				char c = ' ';
				c = ((i != 0) ? array[random.Next(array.Length)] : array[random.Next(9, array.Length)]);
				if (char_type == 2)
				{
					if (GetRandNumber(1, 2) == 2)
					{
						stringBuilder.Append(c.ToString().ToUpper());
					}
					else
					{
						stringBuilder.Append(c);
					}
				}
				else
				{
					stringBuilder.Append(c);
				}
			}
			if (char_type == 1)
			{
				return stringBuilder.ToString().ToUpper();
			}
			return stringBuilder.ToString();
		}

		public static string GetChineseName()
		{
			string[] array = new string[157]
			{
				"白",
				"毕",
				"卞",
				"蔡",
				"曹",
				"岑",
				"常",
				"车",
				"陈",
				"成",
				"程",
				"池",
				"邓",
				"丁",
				"范",
				"方",
				"樊",
				"费",
				"冯",
				"符",
				"傅",
				"甘",
				"高",
				"葛",
				"龚",
				"古",
				"关",
				"郭",
				"韩",
				"何",
				"贺",
				"洪",
				"侯",
				"胡",
				"华",
				"黄",
				"霍",
				"姬",
				"简",
				"江",
				"姜",
				"蒋",
				"金",
				"康",
				"柯",
				"孔",
				"赖",
				"郎",
				"乐",
				"雷",
				"黎",
				"李",
				"连",
				"廉",
				"梁",
				"廖",
				"林",
				"凌",
				"刘",
				"柳",
				"龙",
				"卢",
				"鲁",
				"陆",
				"路",
				"吕",
				"罗",
				"骆",
				"马",
				"梅",
				"孟",
				"莫",
				"母",
				"穆",
				"倪",
				"宁",
				"欧",
				"区",
				"潘",
				"彭",
				"蒲",
				"皮",
				"齐",
				"戚",
				"钱",
				"强",
				"秦",
				"丘",
				"邱",
				"饶",
				"任",
				"沈",
				"盛",
				"施",
				"石",
				"时",
				"史",
				"司徒",
				"苏",
				"孙",
				"谭",
				"汤",
				"唐",
				"陶",
				"田",
				"童",
				"涂",
				"王",
				"危",
				"韦",
				"卫",
				"魏",
				"温",
				"文",
				"翁",
				"巫",
				"邬",
				"吴",
				"伍",
				"武",
				"席",
				"夏",
				"萧",
				"谢",
				"辛",
				"邢",
				"徐",
				"许",
				"薛",
				"严",
				"颜",
				"杨",
				"叶",
				"易",
				"殷",
				"尤",
				"于",
				"余",
				"俞",
				"虞",
				"元",
				"袁",
				"岳",
				"云",
				"曾",
				"詹",
				"张",
				"章",
				"赵",
				"郑",
				"钟",
				"周",
				"邹",
				"朱",
				"褚",
				"庄",
				"卓"
			};
			string text = "匕刁丐歹戈夭仑讥冗邓艾夯凸卢叭叽皿凹囚矢乍尔冯玄邦迂邢芋芍吏夷吁吕吆屹廷迄臼仲伦伊肋旭匈凫妆亥汛讳讶讹讼诀弛阱驮驯纫玖玛韧抠扼汞扳抡坎坞抑拟抒芙芜苇芥芯芭杖杉巫杈甫匣轩卤肖吱吠呕呐吟呛吻吭邑囤吮岖牡佑佃伺囱肛肘甸狈鸠彤灸刨庇吝庐闰兑灼沐沛汰沥沦汹沧沪忱诅诈罕屁坠妓姊妒纬玫卦坷坯拓坪坤拄拧拂拙拇拗茉昔苛苫苟苞茁苔枉枢枚枫杭郁矾奈奄殴歧卓昙哎咕呵咙呻咒咆咖帕账贬贮氛秉岳侠侥侣侈卑刽刹肴觅忿瓮肮肪狞庞疟疙疚卒氓炬沽沮泣泞泌沼怔怯宠宛衩祈诡帚屉弧弥陋陌函姆虱叁绅驹绊绎契贰玷玲珊拭拷拱挟垢垛拯荆茸茬荚茵茴荞荠荤荧荔栈柑栅柠枷勃柬砂泵砚鸥轴韭虐昧盹咧昵昭盅勋哆咪哟幽钙钝钠钦钧钮毡氢秕俏俄俐侯徊衍胚胧胎狰饵峦奕咨飒闺闽籽娄烁炫洼柒涎洛恃恍恬恤宦诫诬祠诲屏屎逊陨姚娜蚤骇耘耙秦匿埂捂捍袁捌挫挚捣捅埃耿聂荸莽莱莉莹莺梆栖桦栓桅桩贾酌砸砰砾殉逞哮唠哺剔蚌蚜畔蚣蚪蚓哩圃鸯唁哼唆峭唧峻赂赃钾铆氨秫笆俺赁倔殷耸舀豺豹颁胯胰脐脓逛卿鸵鸳馁凌凄衷郭斋疹紊瓷羔烙浦涡涣涤涧涕涩悍悯窍诺诽袒谆祟恕娩骏琐麸琉琅措捺捶赦埠捻掐掂掖掷掸掺勘聊娶菱菲萎菩萤乾萧萨菇彬梗梧梭曹酝酗厢硅硕奢盔匾颅彪眶晤曼晦冕啡畦趾啃蛆蚯蛉蛀唬啰唾啤啥啸崎逻崔崩婴赊铐铛铝铡铣铭矫秸秽笙笤偎傀躯兜衅徘徙舶舷舵敛翎脯逸凰猖祭烹庶庵痊阎阐眷焊焕鸿涯淑淌淮淆渊淫淳淤淀涮涵惦悴惋寂窒谍谐裆袱祷谒谓谚尉堕隅婉颇绰绷综绽缀巢琳琢琼揍堰揩揽揖彭揣搀搓壹搔葫募蒋蒂韩棱椰焚椎棺榔椭粟棘酣酥硝硫颊雳翘凿棠晰鼎喳遏晾畴跋跛蛔蜒蛤鹃喻啼喧嵌赋赎赐锉锌甥掰氮氯黍筏牍粤逾腌腋腕猩猬惫敦痘痢痪竣翔奠遂焙滞湘渤渺溃溅湃愕惶寓窖窘雇谤犀隘媒媚婿缅缆缔缕骚瑟鹉瑰搪聘斟靴靶蓖蒿蒲蓉楔椿楷榄楞楣酪碘硼碉辐辑频睹睦瞄嗜嗦暇畸跷跺蜈蜗蜕蛹嗅嗡嗤署蜀幌锚锥锨锭锰稚颓筷魁衙腻腮腺鹏肄猿颖煞雏馍馏禀痹廓痴靖誊漓溢溯溶滓溺寞窥窟寝褂裸谬媳嫉缚缤剿赘熬赫蔫摹蔓蔗蔼熙蔚兢榛榕酵碟碴碱碳辕辖雌墅嘁踊蝉嘀幔镀舔熏箍箕箫舆僧孵瘩瘟彰粹漱漩漾慷寡寥谭褐褪隧嫡缨撵撩撮撬擒墩撰鞍蕊蕴樊樟橄敷豌醇磕磅碾憋嘶嘲嘹蝠蝎蝌蝗蝙嘿幢镊镐稽篓膘鲤鲫褒瘪瘤瘫凛澎潭潦澳潘澈澜澄憔懊憎翩褥谴鹤憨履嬉豫缭撼擂擅蕾薛薇擎翰噩橱橙瓢蟥霍霎辙冀踱蹂蟆螃螟噪鹦黔穆篡篷篙篱儒膳鲸瘾瘸糙燎濒憾懈窿缰壕藐檬檐檩檀礁磷瞭瞬瞳瞪曙蹋蟋蟀嚎赡镣魏簇儡徽爵朦臊鳄糜癌懦豁臀藕藤瞻嚣鳍癞瀑襟璧戳攒孽蘑藻鳖蹭蹬簸簿蟹靡癣羹鬓攘蠕巍鳞糯譬霹躏髓蘸镶瓤矗";
			Random random = new Random(GetRandomGuid());
			return $"{array[random.Next(array.Length - 1)]}{text.Substring(random.Next(0, text.Length - 1), 1)}{text.Substring(random.Next(0, text.Length - 1), 1)}";
		}

		public static string GenerateSurname()
		{
			string text = string.Empty;
			string[] array = "a,a,a,a,a,e,e,e,e,e,e,e,e,e,e,e,i,i,i,o,o,o,u,y,ee,ee,ea,ea,ey,eau,eigh,oa,oo,ou,ough,ay".Split(',');
			string[] array2 = "s,s,s,s,t,t,t,t,t,n,n,r,l,d,sm,sl,sh,sh,th,th,th".Split(',');
			string[] array3 = "sh,sh,st,st,b,c,f,g,h,k,l,m,p,p,ph,wh".Split(',');
			string[] array4 = "x,ss,ss,ch,ch,ck,ck,dd,kn,rt,gh,mm,nd,nd,nn,pp,ps,tt,ff,rr,rk,mp,ll".Split(',');
			string[] array5 = "j,j,j,v,v,w,w,w,z,qu,qu".Split(',');
			Random random = new Random(GetRandomGuid());
			int[] array6 = new int[10]
			{
				2,
				2,
				2,
				2,
				2,
				2,
				3,
				3,
				3,
				4
			};
			int num = array6[random.Next(array6.Length)];
			for (int i = 0; i < num; i++)
			{
				int num2 = random.Next(1000);
				string[] array7 = (num2 < 775) ? array2 : ((num2 < 875 && i > 0) ? array4 : ((num2 >= 985) ? array5 : array3));
				text += array7[random.Next(array7.Length)];
				text += array[random.Next(array.Length)];
				if ((text.Length > 4 && random.Next(1000) < 800) || (text.Length > 6 && random.Next(1000) < 950) || text.Length > 7)
				{
					break;
				}
			}
			int num3 = random.Next(1000);
			num3 = ((text.Length <= 6) ? (num3 + text.Length * 10) : (num3 - text.Length * 25));
			if (num3 >= 400)
			{
				if (num3 < 775)
				{
					text += array2[random.Next(array2.Length)];
				}
				else if (num3 < 825)
				{
					text += array3[random.Next(array3.Length)];
				}
				else if (num3 < 840)
				{
					text += "ski";
				}
				else if (num3 < 860)
				{
					text += "son";
				}
				else
				{
					if (Regex.IsMatch(text, "(.+)(ay|e|ee|ea|oo)$") || text.Length < 5)
					{
						return "Mc" + text.Substring(0, 1).ToUpper() + text.Substring(1);
					}
					text += "ez";
				}
			}
			return text.Substring(0, 1).ToUpper() + text.Substring(1);
		}

		public static int GetRandomGuid()
		{
			return Guid.NewGuid().GetHashCode();
		}

		public static bool IsJson(this string input)
		{
			if (input.IsNull())
			{
				return false;
			}
			input = input.Trim();
			if (!input.StartsWith("{") || !input.EndsWith("}"))
			{
				if (input.StartsWith("["))
				{
					return input.EndsWith("]");
				}
				return false;
			}
			return true;
		}

		public static bool IsNull(this string text)
		{
			if (text == null)
			{
				return true;
			}
			text = Regex.Replace(text, "[\r\n]|[\t]", "");
			if (string.IsNullOrWhiteSpace(text))
			{
				return true;
			}
			return false;
		}

		public static bool IsUpper(string str)
		{
			if (new Regex("^[A-Z]+$").IsMatch(str))
			{
				return true;
			}
			return false;
		}

		public static bool IsLower(string str)
		{
			if (new Regex("^[a-z]+$").IsMatch(str))
			{
				return true;
			}
			return false;
		}

		public static bool IsInt(string str)
		{
			if (new Regex("^[0-9]+$").IsMatch(str))
			{
				return true;
			}
			return false;
		}

		public static int ToInt32(this string s)
		{
			int.TryParse(s, out int result);
			return result;
		}

		public static long ToInt64(this string s)
		{
			long.TryParse(s, out long result);
			return result;
		}

		public static double ToDouble(this string s)
		{
			double.TryParse(s, out double result);
			return result;
		}

		public static decimal ToDecimal(this string s)
		{
			decimal.TryParse(s, out decimal result);
			return result;
		}

		public static decimal ToDecimal(this double s)
		{
			return new decimal(s);
		}

		public static double ToDouble(this decimal s)
		{
			return (double)s;
		}

		public static int ToInt32(this double num)
		{
			return (int)Math.Floor(num);
		}

		public static int ToInt32(this decimal num)
		{
			return (int)Math.Floor(num);
		}

		public static long ToLong(this string str, long defaultResult)
		{
			if (!long.TryParse(str, out long result))
			{
				return defaultResult;
			}
			return result;
		}

		public static double ToDouble(this int num)
		{
			return (double)num * 1.0;
		}

		public static decimal ToDecimal(this int num)
		{
			return (decimal)((double)num * 1.0);
		}

		public static string RemoveEmpty(this string text)
		{
			return text.Trim(new char[] { '\r', '\n', ' ' });
		}
	}
}
