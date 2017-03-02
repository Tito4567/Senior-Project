package ash.laces;

import android.graphics.drawable.Icon;
import android.media.Image;

import java.util.ArrayList;
import java.util.Iterator;
import java.util.List;

/**
 * Created by Ash on 3/1/17.
 *
 * - A new user account object is created whenever a new user creates an account.
 */

public class UserAccount {
    //FIELDS
    private String username, password, hashpass, bio, displayedName;
    private int numPosts, numFollowing, numFollowers;
    private Icon icon;
    //private Image icon; ????

    private ArrayList<Post> postList;
    private Iterator<Post> postItr;

    private ArrayList<UserAccount> followingList;
    private Iterator<UserAccount> followingItr;

    private ArrayList<UserAccount> followersList;
    private Iterator<UserAccount> followiersItr;

    //CONSTRUCTOR
    public UserAccount(String username, String password, String hashpass, String displayedName) {
        this.username = username;
        this.password = password;
        this.hashpass = hashpass;
        this.displayedName = displayedName;

        this.bio = null;

        numPosts = 0;
        numFollowing = 0;
        numFollowers = 0;

        icon = null;

        postList = new ArrayList<Post>();
        postItr = postList.iterator();

        followingList = new ArrayList<UserAccount>();
        followingItr = followingList.iterator();

        followersList = new ArrayList<UserAccount>();
        followiersItr = followersList.iterator();

    }

    //METHODS
    public String getUsername() {
        return this.username;
    }

    public void setUsername(String username) {
        this.username = username;
    }

    public String getPassword() {
        return password;
    }

    public void setPassword(String password) {
        this.password = password;
    }

    public String getHashpass() {
        return hashpass;
    }

    public void setHaspass(String hashpass) {
        this.hashpass = hashpass;
    }

    public String getDisplayedName() {
        return displayedName;
    }

    public void setDisplayedName(String displayedName) {
        this.displayedName = displayedName;
    }

    public String getBio() {
        return bio;
    }

    public void setBio(String bio) {
        this.bio = bio;
    }

    public int getNumPosts() {
        return numPosts;
    }

    public void incrementNumPosts() {
        this.numPosts++;
    }

    public int getNumFollowing() {
        return numFollowing;
    }

    public void incrementNumFollowing() {
        this.numFollowing++;
    }

    public int getNumFollower() {
        return numFollowers;
    }

    public void incrementNumFollowers() {
        this.numFollowers++;
    }

    public Icon getIcon() {
        return icon;
    }

    public void setIcon(Icon icon) {
        this.icon = icon;
    }

    public ArrayList<Post> getPosts() {
        return postList;
    }

    public void addToPosts(Post post) {
        postList.add(post);
    }

    public ArrayList<UserAccount> getFollowing() {
        return followingList;
    }

    public void addToFollowing(UserAccount account) {
        followingList.add(account);
        incrementNumFollowing();
    }

    public ArrayList<UserAccount> getFollowers() {
        return followersList;
    }

    public void addToFollowers(UserAccount account) {
        followersList.add(account);
        incrementNumFollowers();
    }


}
