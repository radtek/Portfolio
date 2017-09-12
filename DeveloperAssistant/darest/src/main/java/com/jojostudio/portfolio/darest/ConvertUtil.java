package com.jojostudio.portfolio.darest;

import java.util.HashMap;
import java.util.Map;

public class ConvertUtil {
	//Number to Hexadecimal
    public static String toHex(int num) {
        char[] map = {'0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F'};
        
        if (num == 0) {
            return "0";
        }
        
        String result = "";
        while (num != 0) {
            result = map[num & 15] + result;
            num = num >>> 4;
        }
        
        return result;
    }
    
    //Integer to English Words
    //123 -> "One Hundred Twenty Three"
    ///12345 -> "Twelve Thousand Three Hundred Forty Five"
    //1234567 -> "One Million Two Hundred Thirty Four Thousand Five Hundred Sixty Seven"
    private static final String[] LESS_THAN_20 = {"", "One", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine", "Ten", "Eleven", "Twelve", "Thirteen", "Fourteen", "Fifteen", "Sixteen", "Seventeen", "Eighteen", "Nineteen"};
    private static final String[] TENS = {"", "Ten", "Twenty", "Thirty", "Forty", "Fifty", "Sixty", "Seventy", "Eighty", "Ninety"};
    private static final String[] THOUSANDS = {"Billion", "Million", "Thousand", ""};
    private static final int[] radix = {1000000000, 1000000, 1000, 1};
    public static String numberToWords(int num) {
        if (num <= 0) {
            return "Zero";
        }
        
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < radix.length; i++) {
            if (num / radix[i] == 0) {
                continue;
            }

            sb.append(helper(num / radix[i])).append(THOUSANDS[i]).append(" ");
            num = num % radix[i];
        }
        return sb.toString().trim();
    }
    
    private static String helper(int num) {
        if (num == 0) {
            return "";
        }
        if (num < 20) {
            return LESS_THAN_20[num] + " ";
        }
        if (num < 100) {
            return TENS[num / 10] + " " + helper(num % 10);
        }
        return LESS_THAN_20[num / 100] + " Hundred " + helper(num % 100);
    }
    
    //Integer to Roman
    public static String intToRoman(int num) {
        if (num <= 0) {
            return "";
        }
        String[] dict = new String[]{"M","CM","D","CD","C","XC","L","XL","X","IX","V","IV","I"};
        int[] val = {1000,900,500,400,100,90,50,40,10,9,5,4,1};  
        
        StringBuilder sb = new StringBuilder();
        for(int i = 0; i < 13; i++) {
            if(num >= val[i]) {
                int count = num / val[i];                
                for(int j = 0; j < count; j++) {
                    sb.append(dict[i]);
                }
                num %= val[i];
            }
        }
        return sb.toString();
    }
    
    //Roman to Integer
    public static int romanToInt(String s) {
        if (s == null || s.isEmpty()) {
            return 0;
        }
        
        Map<Character, Integer> map = new HashMap<Character, Integer>();
        map.put('I', 1);
        map.put('V', 5);
        map.put('X', 10);
        map.put('L', 50);
        map.put('C', 100);
        map.put('D', 500);
        map.put('M', 1000);

        int len = s.length();
        int res = map.get(s.charAt(len - 1));
        for (int i = len - 2; i >= 0; i--) {
            if (map.get(s.charAt(i + 1)) <= map.get(s.charAt(i))) {
                res += map.get(s.charAt(i));
            } else {
                res -= map.get(s.charAt(i));
            }
        }
        return res;        
    }
}
