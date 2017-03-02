package ash.laces;

import android.graphics.drawable.Icon;
import android.media.Image;

import java.lang.reflect.Array;
import java.util.ArrayList;
import java.util.Iterator;

/**
 * Created by Ash on 3/1/17.
 *
 * - A new Post object is created whenever a new post is made.
 *
 * - An unselected post shows only the OP, OP's icon, distance,
 *   number of likes, number of comments, number of pictures, and pictures
 *
 * - A selected post shows everything an unselected post does, as well as the model,
 *   brand, condition, comment, size, release date, and asking price; all of
 *   which are filled out by the OP.
 */

public class Post {
    //FIELDS
    private PostType type;
    private boolean isSelected;

    //Unselected post info
    private UserAccount OP;
    private Icon icon;
    private int distance, numComments, numLikes, numPictures;
    private Image image1, image2, image3, image4;
    private ArrayList<Comment> commentList;
    private Iterator<Comment> commentItr;
    private ArrayList<UserAccount> likeList;
    private Iterator<UserAccount> likeItr;

    //Selected post info; more detailed than unselected post
    private String model, brand, condition, description;
    private int size, releaseDate, askingPrice;

    //CONSTRUCTOR
    public Post(PostType type, Image image1, Image image2, Image image3, Image image4, String model,
                    String brand, String condition, String description, int size, int releaseDate, int askingPrice) {
        this.type = type;
        isSelected = false;

        icon = null;
        distance = 30; //Just a placeholder
        numComments = 0;
        numLikes = 0;
        numPictures = 4;
        this.image1 = image1;
        this.image2 = image2;
        this.image3 = image3;
        this.image4 = image4;
        commentList = new ArrayList<Comment>();
        likeList = new ArrayList<UserAccount>();

        this.model = model;
        this.brand = brand;
        this.condition = condition;
        this.description = description;
        this.size = size;
        this.releaseDate = releaseDate;
        this.askingPrice = askingPrice;
    }

    //METHODS

    public UserAccount getOP() {
        return OP;
    }

    public void setOP(UserAccount OP) {
        this.OP = OP;
    }

    public Icon getIcon() {
        return icon;
    }

    public void setIcon(Icon icon) {
        this.icon = icon;
    }

    public int getDistance() {
        return distance;
    }

    public void setDistance(int distance) {
        this.distance = distance;
    }

    public int getNumComments() {
        return numComments;
    }

    public void incrementNumComments() {
        numComments++;
    }

    public int getNumLikes() {
        return numLikes;
    }

    public void incrementNumLikes() {
        numLikes++;
    }

    public int getNumPictures() {
        return numPictures;
    }

    public void setNumPictures(int numPictures) {
        this.numPictures = numPictures;
    }

    public Image getImage1() {
        return image1;
    }

    public void setImage1(Image image1) {
        this.image1 = image1;
    }

    public Image getImage2() {
        return image2;
    }

    public void setImage2(Image image2) {
        this.image2 = image2;
    }

    public Image getImage3() {
        return image3;
    }

    public void setImage3(Image image3) {
        this.image3 = image3;
    }

    public Image getImage4() {
        return image4;
    }

    public void setImage4(Image image4) {
        this.image4 = image4;
    }

    public ArrayList<Comment> getCommentList() {
        return commentList;
    }

    public void addComment(Comment comment) {
        commentList.add(comment);
        incrementNumComments();
    }

    public Iterator<Comment> getCommentItr() {
        return commentItr;
    }

    public void setCommentItr(Iterator<Comment> commentItr) {
        this.commentItr = commentItr;
    }

    public ArrayList<UserAccount> getLikeList() {
        return likeList;
    }

    public void addLike(UserAccount like) {
        likeList.add(like);
        incrementNumLikes();
    }

    public Iterator<UserAccount> getLikeItr() {
        return likeItr;
    }

    public void setLikeItr(Iterator<UserAccount> likeItr) {
        this.likeItr = likeItr;
    }

    public String getModel() {
        return model;
    }

    public void setModel(String model) {
        this.model = model;
    }

    public String getBrand() {
        return brand;
    }

    public void setBrand(String brand) {
        this.brand = brand;
    }

    public String getCondition() {
        return condition;
    }

    public void setCondition(String condition) {
        this.condition = condition;
    }

    public String getDescription() {
        return description;
    }

    public void setDescription(String description) {
        this.description = description;
    }

    public int getSize() {
        return size;
    }

    public void setSize(int size) {
        this.size = size;
    }

    public int getReleaseDate() {
        return releaseDate;
    }

    public void setReleaseDate(int releaseDate) {
        this.releaseDate = releaseDate;
    }

    public int getAskingPrice() {
        return askingPrice;
    }

    public void setAskingPrice(int askingPrice) {
        this.askingPrice = askingPrice;
    }

    public boolean isSelected() {
        return isSelected;
    }

    public void setSelected(boolean selected) {
        isSelected = selected;
    }

    public PostType getType() {
        return type;
    }

    public void setType(PostType type) {
        this.type = type;
    }
}
