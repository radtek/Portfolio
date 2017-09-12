package com.jojostudio.portfolio.darest.controller;

import org.springframework.web.bind.annotation.PathVariable;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestMethod;
import org.springframework.web.bind.annotation.RestController;

import com.jojostudio.portfolio.darest.ConvertUtil;
import com.jojostudio.portfolio.darest.StringHelper;
import com.jojostudio.portfolio.darest.domain.ResEntity;

@RestController
@RequestMapping("/number")
public class NumberController {
	//Number to Hexadecimal
	@RequestMapping(value="/tohex/{source}", method = RequestMethod.GET)
    public ResEntity toHex(@PathVariable(value="source") int source) {
		ResEntity sr = new ResEntity();
        sr.setResp(ConvertUtil.toHex(source));
        return sr;
    }
	
	//Integer to English Words
    @RequestMapping("/toenglish/{source}")
    public ResEntity toEnglishWords(@PathVariable(value="source") int source) {
    		ResEntity sr = new ResEntity();
    		sr.setResp(ConvertUtil.numberToWords(source));
        return sr;
    }
    
    //Integer to Roman
    @RequestMapping("/inttoroman/{source}")
    public ResEntity toRoman(@PathVariable(value="source") int source) {
    		ResEntity sr = new ResEntity();
    		sr.setResp(ConvertUtil.intToRoman(source));
        return sr;
    }
	//Integer to English Words
    @RequestMapping("/romantoint/{source}")
    public ResEntity toEnglishWords(@PathVariable(value="source") String source) {
    		ResEntity sr = new ResEntity();
    		sr.setResp(StringHelper.convertToString(ConvertUtil.romanToInt(source)));
        return sr;
    }
}
