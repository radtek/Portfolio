package com.jojostudio.portfolio.darest.controller;

import org.springframework.web.bind.annotation.PathVariable;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestMethod;
import org.springframework.web.bind.annotation.RestController;

import com.jojostudio.portfolio.darest.domain.ResEntity;

import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;

@RestController
@RequestMapping("/string")
public class StringController {
	@RequestMapping(value="/reverse/{source}", method = RequestMethod.GET)
    public ResponseEntity<ResEntity> reverse(@PathVariable(value="source") String source) {
		ResEntity sr = new ResEntity();
        if (source == null || source.isEmpty()) {
            return new ResponseEntity<ResEntity>(sr, HttpStatus.OK);
        }
        String reversed = new StringBuilder(source).reverse().toString();
        sr.setResp(reversed);
        return new ResponseEntity<ResEntity>(sr, HttpStatus.OK);
    }
	
    @RequestMapping("/replace/{source}/{from}/{to}")
    public ResponseEntity<ResEntity> reverse(@PathVariable(value="source") String source,
                          @PathVariable(value="from") String from,
                          @PathVariable(value="to") String to) {
    		ResEntity sr = new ResEntity();
        if (source == null || source.isEmpty() ||
            from == null || from.isEmpty() ||
            to == null || to.isEmpty()) {
        		sr.setResp(source);
        } else {
        		sr.setResp(source.replaceFirst(to, from));
        }
        return new ResponseEntity<ResEntity>(sr, HttpStatus.OK);
    }
}
