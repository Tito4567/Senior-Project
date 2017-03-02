package ash.laces;

/**
 * Created by Ash on 3/1/17.
 *
 * - Not really much to say about this? Each comment has a user associated
 *   with it, the user that made the comment.
 */

public class Comment {
    //FIELDS
    String comment;
    UserAccount OC; //Original Commenter

    //CONSTRUCTOR
    public Comment(String comment, UserAccount user) {
        this.comment = comment;
        this.OC = user;
    }

    //METHODS
    public String getComment() {
        return comment;
    }

    public void editComment(String newComment) {
        this.comment = newComment;
    }

    public UserAccount getOC() { return OC; }
}
