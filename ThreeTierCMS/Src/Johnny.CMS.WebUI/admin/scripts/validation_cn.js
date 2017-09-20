/**
 * 
 * 新增验证:
 * 增加min-value-number验证,例: 最小值为10.1 = min-value-10.1
 * 增加max-value-number验证,例: 最大值为-100.1 = min-value--100.1
 * 增加长度范围validate-length-range-minLength-maxLength验证,例: 最小长度为1,最大长度为10 = validate-length-range-1-10
 * 增加整型数字范围validate-int-range-minValue-maxValue验证,例: 5至20 = validate-int-range-5-20
 * 增加浮点数字范围validate-float-range-minValue-maxValue验证,例: -1.1至10 = validate-float-range--1.1-10
 * 增加min-length-number验证,例: 最小长度为10 = min-length-10
 * 增加max-length-number验证,例: 最大长度为10 = max-length-10
 * 增加文件类型 validate-file-type1-type2-typeX 的验证,例: validate-file-zip-png-jpeg 将验证文件是否为zip,png,jpeg格式之一
 * 增加中文日期验证 validate-date-cn
 * 增加相等验证validate-equals-item1-item2-itemX,判断输入的值为[item1,item2,itemX]之一
 * 
 * 修改记录:
 * 增加Validation._getInputValue() 取代$F()方法以对file input进行验证
 * 修正Validation.isVisible() 中while循环中elm可能不存在为空的问题
 * 增加ValidationFactory for cache
 * 修改Validation.get()方法使用indexOf()的匹配模式,以适应可以通过class传递参数
 * 修改errorMsg可由方法返回
 * 增加Validation._getErrorMsg()方法
 * 修改advice可以动态修改
 * 
 * by badqiu (badqiu@gmail.com)
 */

/*
 * Really easy field validation with Prototype
 * http://tetlaw.id.au/view/blog/really-easy-field-validation-with-prototype
 * Andrew Tetlaw
 * Version 1.5.3 (2006-07-15)
 * 
 * Copyright (c) 2006 Andrew Tetlaw
 * http://www.opensource.org/licenses/mit-license.php
 */
Validator = Class.create();

Validator.prototype = {
	initialize : function(pattern, error, test, options) {
		this.options = Object.extend({}, options || {});
		this._test = test ? test : function(v,elm){ return true };
		this.error = error ? error : 'Validation failed.';
		this.pattern = pattern;
	},
	test : function(v, elm) {
		return this._test(v,elm);
	}
}

var Validation = Class.create();

Validation.prototype = {
	initialize : function(form, options){
		this.options = Object.extend({
			onSubmit : true,
			stopOnFirst : false,
			immediate : false,
			focusOnError : true,
			useTitles : false,
			onFormValidate : function(result, form) {},
			onElementValidate : function(result, elm) {}
		}, options || {});
		this.form = $(form);
		if(this.options.onSubmit) Event.observe(this.form,'submit',this.onSubmit.bind(this),false);
		if(this.options.immediate) {
			var useTitles = this.options.useTitles;
			var callback = this.options.onElementValidate;
			Form.getElements(this.form).each(function(input) { // Thanks Mike!
				Event.observe(input, 'blur', function(ev) { Validation.validate(Event.element(ev),{useTitle : useTitles, onElementValidate : callback}); });
			});
		}
	},
	onSubmit :  function(ev){
		if(!this.validate()) Event.stop(ev);
	},
	validate : function() {
		var result = false;
		var useTitles = this.options.useTitles;
		var callback = this.options.onElementValidate;
		if(this.options.stopOnFirst) {
			result = Form.getElements(this.form).all(function(elm) { return Validation.validate(elm,{useTitle : useTitles, onElementValidate : callback}); });
		} else {
			result = Form.getElements(this.form).collect(function(elm) { return Validation.validate(elm,{useTitle : useTitles, onElementValidate : callback}); }).all();
		}
		if(!result && this.options.focusOnError) {
			Form.getElements(this.form).findAll(function(elm){return $(elm).hasClassName('validation-failed')}).first().focus()
		}
		this.options.onFormValidate(result, this.form);
		return result;
	},
	reset : function() {
		Form.getElements(this.form).each(Validation.reset);
	},
	initial : function() {		
	
		var controls=this.form.elements;

		for(i=0;i<controls.length;i++)
		{
		   var control=controls[i];
		   if(control.attributes["tip"]&&control.attributes["pattern"])
		   {
			   var controlTip=control.attributes["tip"].value;
			   if(controlTip!=null)
			   {			 
				   Validation.changemessageclass(control,"msgNormal");			   
			   }
		   }
		}
	}
}

Object.extend(Validation, {
	validate : function(elm, options){
		options = Object.extend({
			useTitle : false,
			onElementValidate : function(result, elm) {}
		}, options || {});
		elm = $(elm);
		/*
		var cn = elm.classNames();
		return result = cn.all(function(value) {
			var test = Validation.test(value,elm,options.useTitle);
			options.onElementValidate(test, elm);
			return test;
		});
		*/
		if (elm.attributes["pattern"])
		{
			var arrPattern = elm.attributes["pattern"].value.split(" "); /* 兼容firefox，而不用elm.pattern.split(" ");*/
			for (var ix = 0; ix < arrPattern.length; ix++)
			{
				var test = Validation.test(arrPattern[ix],elm,options.useTitle);
				options.onElementValidate(test, elm);
				if(!test)
					return test;
			}
		}
	},
	_getInputValue : function(elm) {
		var elm = $(elm);
		if(elm.type.toLowerCase() == 'file') {
			return elm.value;
		}else {
			return $F(elm);
		}
	},
	_getErrorMsg : function(useTitle,elm,error) {
		if( typeof(error) == 'function' ) {
			error = error(Validation._getInputValue(elm),elm);
		}
		return useTitle ? ((elm && elm.title) ? elm.title : error) : error;
	},
	test : function(name, elm, useTitle) {
		var v = Validation.get(name);
		var prop = '__advice'+name.camelize();
		if(Validation.isVisible(elm) && !v.test(Validation._getInputValue(elm),elm)) {
			/*
			if(!elm[prop]) {
				var advice = Validation.getAdvice(name, elm);
				if(typeof advice == 'undefined') {
					var errorMsg = Validation._getErrorMsg(useTitle,elm,v.error);
					advice = '<div class="validation-advice" id="advice-' + name + '-' + Validation.getElmID(elm) +'" style="display:none">' + errorMsg + '</div>'
					switch (elm.type.toLowerCase()) {
						case 'checkbox':
						case 'radio':
							var p = elm.parentNode;
							if(p) {
								new Insertion.Bottom(p, advice);
							} else {
								new Insertion.After(elm, advice);
							}
							break;
						default:
							new Insertion.After(elm, advice);
				    }
					advice = $('advice-' + name + '-' + Validation.getElmID(elm));
				}				
				if(typeof Effect == 'undefined') {
					advice.style.display = 'block';
				} else {
					new Effect.Appear(advice, {duration : 1 });
				}
			}
			
			var advice = Validation.getAdvice(name, elm);
			advice.innerHTML = Validation._getErrorMsg(useTitle,elm,v.error);
			*/
			
			elm[prop] = true;
			elm.removeClassName('validation-passed');
			elm.addClassName('validation-failed');
			if(elm.type=='select-multiple'||elm.type=='select-one')
			{				
				var parent = elm.parentNode;
				parent.removeClassName('validation-passed');
				parent.addClassName('validation-failed');
			}
			
			var messageTip="";
			if(elm.type=="radio"||elm.type=="checkbox")
				messageTip=$(elm.id.substring(0,elm.id.length-1)+'0Tip');
			else
				messageTip=$(elm.id+'Tip');
				//alert(messageTip.id)
			if(typeof messageTip != 'undefined')
			{
				messageTip.innerHTML = Validation._getErrorMsg(useTitle,elm,v.error);
				messageTip.removeClassName('msgOK');
				messageTip.addClassName('msgError');
			}
			return false;
		} else {
			var advice = Validation.getAdvice(name, elm);
			if(typeof advice != 'undefined') advice.hide();
			elm[prop] = '';
			elm.removeClassName('validation-failed');
			elm.addClassName('validation-passed');
			if(elm.type=='select-multiple'||elm.type=='select-one')
			{				
				var parent = elm.parentNode;
				parent.removeClassName('validation-failed');
				parent.addClassName('validation-passed');
			}
			var messageTip="";
			if(elm.type=="radio"||elm.type=="checkbox")
			{
				messageTip=$(elm.id.substring(0,elm.id.length-1)+'0Tip');				
				messageTip.innerHTML = messageTip.attributes["tip"].value;
			}
			else
			{
				messageTip=$(elm.id+'Tip');
				messageTip.innerHTML = elm.attributes["tip"].value;
			}
			messageTip.removeClassName('msgError');
			messageTip.addClassName('msgOK');			
			return true;
		}
	},
	isVisible : function(elm) {
		while(elm && elm.tagName != 'BODY') {
			if(!$(elm).visible()) return false;
			elm = elm.parentNode;
		}
		return true;
	},
	getAdvice : function(name, elm) {
		return Try.these(
			function(){ return $('advice-' + name + '-' + Validation.getElmID(elm)) },
			function(){ return $('advice-' + Validation.getElmID(elm)) }
		);
	},
	getElmID : function(elm) {
		return elm.id ? elm.id : elm.name;
	},
	reset : function(elm) {
		elm = $(elm);
		/*
		var cn = elm.classNames();
		cn.each(function(value) {
			var prop = '__advice'+value.camelize();
			if(elm[prop]) {
				var advice = Validation.getAdvice(value, elm);
				advice.hide();
				elm[prop] = '';
			}
			elm.removeClassName('validation-failed');
			elm.removeClassName('validation-passed');
		});
		*/
		var arrPattern = elm.attributes["pattern"].value.split(" "); /* 兼容firefox，而不用elm.pattern.split(" ");*/
		for (var ix = 0; ix < arrPattern.length; ix++)
		{
			var prop = '__advice'+arrPattern[ix].camelize();
			if(elm[prop]) {
				var advice = Validation.getAdvice(arrPattern[ix], elm);
				advice.hide();
				elm[prop] = '';
			}
			elm.removeClassName('validation-failed');
			elm.removeClassName('validation-passed');		
			//var messageTip=$(elm.id+'Tip');
			//messageTip.innerHTML = elm.tip;
		}
	},
	add : function(pattern, error, test, options) {
		var nv = {};
		nv[pattern] = new Validator(pattern, error, test, options);
		Object.extend(Validation.methods, nv);
	},
	addAllThese : function(validators) {
		var nv = {};
		$A(validators).each(function(value) {
				nv[value[0]] = new Validator(value[0], value[1], value[2], (value.length > 3 ? value[3] : {}));
			});
		Object.extend(Validation.methods, nv);
	},
	get : function(name) {
		var resultMethodName;
		for(var methodName in Validation.methods) {
			if(name == methodName) {
				resultMethodName = methodName;
				break;
			}
			if(name.indexOf(methodName) >= 0) {
				resultMethodName = methodName;
			}
		}
		return Validation.methods[resultMethodName] ? Validation.methods[resultMethodName] : new Validator();
		//return  Validation.methods[name] ? Validation.methods[name] : new Validator();
	},
	changemessageclass : function(control,className) {
		var messageTip=$(control.id+'Tip');
		messageTip.className =className;
		messageTip.innerHTML=control.attributes["tip"].value;
	},
	
	methods : {}
});

Validation.add('IsEmpty', '', function(v) {
				return  ((v == null) || (v.length == 0)); // || /^\s+$/.test(v));
			});

Validation.addAllThese([
	['required', '这里不能为空.', function(v) {
				return !Validation.get('IsEmpty').test(v);
			}],
	['validate-number', '请输入正确的数字', function(v) {
				return Validation.get('IsEmpty').test(v) || (!isNaN(v) && !/^\s+$/.test(v));
			}],
	['validate-digits', '请输入一个数字. 避免输入空格,逗号,分号等字符', function(v) {
				return Validation.get('IsEmpty').test(v) ||  !/[^\d]/.test(v);
			}],
	['validate-alpha', '请输入[a-z]的字母', function (v) {
				return Validation.get('IsEmpty').test(v) ||  /^[a-zA-Z]+$/.test(v)
			}],
	['validate-alphanum', '请输入[a-z]的字母或是[0-9]的数字,其它字符是不允许的.', function(v) {
				return Validation.get('IsEmpty').test(v) ||  !/\W/.test(v)
			}],
	['validate-date', '请输入有效的日期', function(v) {
				var test = new Date(v);
				return Validation.get('IsEmpty').test(v) || !isNaN(test);
			}],
	['validate-email', '请输入有效的邮件地址,如 username@example.com .', function (v) {
				return Validation.get('IsEmpty').test(v) || /\w{1,}[@][\w\-]{1,}([.]([\w\-]{1,})){1,3}$/.test(v)
			}],
	['validate-url', '请输入有效的URL地址.', function (v) {
				return Validation.get('IsEmpty').test(v) || /^(http|https|ftp):\/\/(([A-Z0-9][A-Z0-9_-]*)(\.[A-Z0-9][A-Z0-9_-]*)+)(:(\d+))?\/?/i.test(v)
			}],
	['validate-date-au', 'Please use this date format: dd/mm/yyyy. For example 17/03/2006 for the 17th of March, 2006.', function(v) {
				if(Validation.get('IsEmpty').test(v)) return true;
				var regex = /^(\d{2})\/(\d{2})\/(\d{4})$/;
				if(!regex.test(v)) return false;
				var d = new Date(v.replace(regex, '$2/$1/$3'));
				return ( parseInt(RegExp.$2, 10) == (1+d.getMonth()) ) && 
							(parseInt(RegExp.$1, 10) == d.getDate()) && 
							(parseInt(RegExp.$3, 10) == d.getFullYear() );
			}],
	['validate-currency-dollar', 'Please enter a valid $ amount. For example $100.00 .', function(v) {
				// [$]1[##][,###]+[.##]
				// [$]1###+[.##]
				// [$]0.##
				// [$].##
				return Validation.get('IsEmpty').test(v) ||  /^\$?\-?([1-9]{1}[0-9]{0,2}(\,[0-9]{3})*(\.[0-9]{0,2})?|[1-9]{1}\d*(\.[0-9]{0,2})?|0(\.[0-9]{0,2})?|(\.[0-9]{1,2})?)$/.test(v)
			}],
	['validate-one-required', '至少选择一个选项.', function (v,elm) {
				var p = elm.parentNode;
				var options = p.getElementsByTagName('INPUT');
				return $A(options).any(function(elm) {
					return $F(elm);
				});
			}]
]);

//ValidationFactory for cache
var ValidationFactory = function(){};
ValidationFactory._cacheValidation = {};
ValidationFactory.create = function(form,options) {
	var inCacheValidation = ValidationFactory._cacheValidation[form];
	if(inCacheValidation)
		return  inCacheValidation;
	var validation = new Validation(form,options);
	ValidationFactory._cacheValidation[form] = validation;
	return validation;
}

//custom validate start
Validation.addAllThese([
	['validate-date-cn', '请使用这样的日期格式: yyyy-mm-dd. 例如:2006-03-17.', function(v) {
				if(Validation.get('IsEmpty').test(v)) return true;
				var regex = /^(\d{4})-(\d{2})-(\d{2})$/;
				if(!regex.test(v)) return false;
				var d = new Date(v.replace(regex, '$1/$2/$3'));
				return ( parseInt(RegExp.$2, 10) == (1+d.getMonth()) ) && 
							(parseInt(RegExp.$3, 10) == d.getDate()) && 
							(parseInt(RegExp.$1, 10) == d.getFullYear() );
			}]
]);
/*
 * Usage: min-length-number
 * Example: min-length-10
 */
Validation.add(
	'min-length', 
	function(v,elm) {
		var results = elm.attributes["pattern"].value.match(/min-length-(\d*)/);
		var minLength = parseInt(results[1]);
		return '最小长度为'+minLength
	},
	function(v,elm) {
		var results = elm.attributes["pattern"].value.match(/min-length-(\d*)/);
		var minLength = parseInt(results[1]);
		return Validation.get('IsEmpty').test(v) || v.length >= minLength
	}
)
/*
 * Usage: max-length-number
 * Example: max-length-10
 */
Validation.add(
	'max-length', 
	function(v,elm) {
		var results = elm.attributes["pattern"].value.match(/max-length-(\d*)/);
		var maxLength = parseInt(results[1]);
		return '最大长度为'+maxLength
	},
	function(v,elm) {
		var results = elm.attributes["pattern"].value.match(/max-length-(\d*)/);
		var maxLength = parseInt(results[1]);
		return Validation.get('IsEmpty').test(v) || v.length <= maxLength
	}
)
/*
 * Usage: validate-file-type1-type2-typeX
 * Example: validate-file-png-jpg-jpeg
 */
Validation.add(
	'validate-file', 
	function(v,elm) {
		var results = elm.attributes["pattern"].value.match(/validate-file-([a-zA-Z0-9-]*)/);
		var extentionNamesStr = results[1];
		var extentionNames = extentionNamesStr.split('-');
		return '文件类型应该为'+extentionNames.join(',');
	},
	function(v,elm) {
		var results = elm.attributes["pattern"].value.match(/validate-file-([a-zA-Z0-9-]*)/);
		var extentionNamesStr = results[1];
		var extentionNames = extentionNamesStr.split('-');
		return Validation.get('IsEmpty').test(v) || extentionNames.any(function(extentionName) {
			var pattern = new RegExp('\\.'+extentionName+'$','i');
			return pattern.test(v);
		});
	}
)

/*
 * Usage: validate-float-range-minValue-maxValue
 * Example: -2.1 to 3 = validate-float-range--2.1-3
 */
Validation.add(
	'validate-float-range', 
	function(v,elm) {
		if(!Validation.get('validate-number').test(v)) {
			return Validation.get('validate-number').error;
		}
		var results = elm.attributes["pattern"].value.match(/validate-float-range-(-?[\d\.]*)-(-?[\d\.]*)/);
		var minValue = parseFloat(results[1]);
		var maxValue = parseFloat(results[2]);
		return '输入值应该为'+minValue+" 至 "+maxValue+"之间";
	},
	function(v,elm) {
		var results = elm.attributes["pattern"].value.match(/validate-float-range-(-?[\d\.]*)-(-?[\d\.]*)/);
		var minValue = parseFloat(results[1]);
		var maxValue = parseFloat(results[2]);
		return Validation.get('IsEmpty').test(v) || (Validation.get('validate-number').test(v) && (parseFloat(v) >= minValue && parseFloat(v) <= maxValue))
	}
)

/*
 * Usage: validate-int-range-minValue-maxValue
 * Example: -10 to 20 = validate-int-range--10-20
 */
Validation.add(
	'validate-int-range', 
	function(v,elm) {
		if(!Validation.get('validate-number').test(v)) {
			return Validation.get('validate-number').error;
		}
		var results = elm.attributes["pattern"].value.match(/validate-int-range-(-?\d*)-(-?\d*)/);
		var minValue = parseInt(results[1]);
		var maxValue = parseInt(results[2]);
		return '输入值应该为'+minValue+' 至 '+maxValue+'之间的整数';
	},
	function(v,elm) {
		var results = elm.attributes["pattern"].value.match(/validate-int-range-(-?\d*)-(-?\d*)/);
		var minValue = parseInt(results[1]);
		var maxValue = parseInt(results[2]);
		return Validation.get('IsEmpty').test(v) || (Validation.get('validate-number').test(v) && (parseInt(v) >= minValue && parseInt(v) <= maxValue))
	} 
)

/*
 * Usage: validate-length-range-minLength-maxLength
 * Example: 10 to 20 = validate-length-range-10-20
 */
Validation.add(
	'validate-length-range', 
	function(v,elm) {
		var results = elm.attributes["pattern"].value.match(/validate-length-range-(\d*)-(\d*)/);
		var minLength = parseInt(results[1]);
		var maxLength = parseInt(results[2]);
		return '长度应该在'+minLength+' - '+maxLength+'之间';
	},
	function(v,elm) {
		var results = elm.attributes["pattern"].value.match(/validate-length-range-(\d*)-(\d*)/);
		var minLength = parseInt(results[1]);
		var maxLength = parseInt(results[2]);
		return Validation.get('IsEmpty').test(v) || (v.length >= minLength && v.length <= maxLength)
	}
)

/*
 * Usage: max-value-number
 * Example: max-value-10
 */
Validation.add(
	'max-value', 
	function(v,elm) {
		if(!Validation.get('validate-number').test(v)) {
			return Validation.get('validate-number').error;
		}
		var results = elm.attributes["pattern"].value.match(/max-value-(-?[\d\.]*)/);
		var value = parseFloat(results[1]);
		return '最大值为'+value;
	},
	function(v,elm) {
		var results = elm.attributes["pattern"].value.match(/max-value-(-?[\d\.]*)/);
		var value = parseFloat(results[1]);
		return Validation.get('IsEmpty').test(v) || (Validation.get('validate-number').test(v) && parseFloat(v) <= value);
	}
)

/*
 * Usage: min-value-number
 * Example: min-value-10
 */
Validation.add(
	'min-value', 
	function(v,elm) {
		if(!Validation.get('validate-number').test(v)) {
			return Validation.get('validate-number').error;
		}
		var results = elm.attributes["pattern"].value.match(/min-value-(-?[\d\.]*)/);
		var value = parseFloat(results[1]);
		return '最小值为'+value;
	},
	function(v,elm) {
		var results = elm.attributes["pattern"].value.match(/min-value-(-?[\d\.]*)/);
		var value = parseFloat(results[1]);
		return Validation.get('IsEmpty').test(v) || (Validation.get('validate-number').test(v) && parseFloat(v) >= value);
	}
)

/*
 * Usage: validate-equals-item1-item2-itemX
 * Example: validate-equals-AA-BB-CC
 */
Validation.add(
	'validate-equals', 
	function(v,elm) {
		var results = elm.attributes["pattern"].value.match(/validate-equals-([\S]*)/);
		var expectedValuesStr = results[1];
		var expectedValues = expectedValuesStr.split('-');
		return '期待的值应该为其中['+expectedValues.join(',')+"]之一";
	},
	function(v,elm) {
		var results = elm.attributes["pattern"].value.match(/validate-equals-([\S]*)/);
		var expectedValuesStr = results[1];
		var expectedValues = expectedValuesStr.split('-');
		return Validation.get('IsEmpty').test(v) || expectedValues.any(function(expectedValue) {
			return v == expectedValue;
		});
	}
)
/*
 * Usage: validate-one-selected
 * Example: validate-one-selected
 */
Validation.add(
	'validate-one-selected', 
	function(v,elm) {
		return '至少选择一个选项';
	},
	function(v,elm) {
		if(elm.selectedIndex == -1 || elm.options[elm.selectedIndex].text == "")
			return false; 
		else
			return true;		
	}
)	