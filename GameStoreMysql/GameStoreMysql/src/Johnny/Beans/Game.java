/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package Johnny.Beans;

/**
 *
 * @author RZHUANG
 */
public class Game extends BaseBean {
    public Game(String key, String maker, String name, double price, String image, String retailer, String condition, double discount){
        super.setKey(key);
        super.setMaker(maker);
        super.setName(name);
        super.setPrice(price);
        super.setImage(image);
        super.setCondition(condition);
        super.setDiscount(discount);
        super.setRetailer(retailer);
    }
}
