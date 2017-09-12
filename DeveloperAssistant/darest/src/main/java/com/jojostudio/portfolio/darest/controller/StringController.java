package com.jojostudio.portfolio.darest.controller;

import org.springframework.web.bind.annotation.PathVariable;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestMethod;
import org.springframework.web.bind.annotation.RestController;

import com.jojostudio.portfolio.darest.domain.ResEntity;

@RestController
@RequestMapping("/string")
public class StringController {
	@RequestMapping(value="/reverse/{source}", method = RequestMethod.GET)
    public ResEntity reverse(@PathVariable(value="source") String source) {
		ResEntity sr = new ResEntity();
        if (source == null || source.isEmpty()) {
            return sr;
        }
        String reversed = new StringBuilder(source).reverse().toString();
        sr.setResp(reversed);
        return sr;
    }
	
    @RequestMapping("/replace/{source}/{from}/{to}")
    public ResEntity replace(@PathVariable(value="source") String source,
                             @PathVariable(value="from") String from,
                             @PathVariable(value="to") String to) {
    		ResEntity sr = new ResEntity();
        if (source == null || source.isEmpty() ||
            from == null || from.isEmpty() ||
            to == null || to.isEmpty()) {
        		sr.setResp(source);
        } else {
        		sr.setResp(source.replaceAll(from, to));
        }
        return sr;
    }
}
