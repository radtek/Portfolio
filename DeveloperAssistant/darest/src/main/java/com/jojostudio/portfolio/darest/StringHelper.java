package com.jojostudio.portfolio.darest;

public class StringHelper {
	public static String convertToString(int num) {
		return String.valueOf(num);
	}
	public static String convertToString(String str) {
		if (str == null) {
			return "";
		}
		return str;
	}
}
