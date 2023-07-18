import { useEffect, useState } from "react";
import { useNavigate, useParams } from "react-router-dom";
import { deletePost, editPost, getPostById } from "../modules/postManager";
import {
    Button,
    Badge,
} from "reactstrap";
import { getAllTags } from "../modules/tagManager";
import {
    addPostTag,
    getPostTagsByPostId,
    deletePostTag,
} from "../modules/postTagManager";
import "./postDetails.css";
import { PostDetailsDeleteModal } from "./PostDetailsDeleteModal";
import { PostDetailsPostTagModal } from "./PostDetailsPostTagModal";
import { PostDetailsEditModal } from "./PostDetailsEditModal";

const PostDetails = ({ userProfile }) => {
    const [post, setPost] = useState();
    const { id } = useParams();
    const [modal, setModal] = useState(false);
    const [modalDelete, setModalDelete] = useState(false);
    const [modalEdit, setModalEdit] = useState(false);
    const [tags, setTags] = useState([]);
    const [postTags, setPostTags] = useState([]);
    const [newPostTag, setNewPostTag] = useState({
        postId: 0,
        tagId: 0,
    });

    const [newPost, updatePost] = useState({
        title: '',
        content: '',
        imageLocation: '',
        createDateTime: null,
        publishDateTime: null,
        isApproved: true,
        categoryId: 0,
        userProfileId: 0
    });

    useEffect(
        () => {
            if (post) {
                const copy = { ...post };
                copy.title = post.title
                copy.content = post.content
                copy.imageLocation = post.imageLocation
                copy.createDateTime = post.createDateTime
                copy.publishDateTime = post.publishDateTime
                copy.isApproved = post.isApproved
                copy.categoryId = post.categoryId
                copy.userProfileId = post.userProfileId
                updatePost(copy);
            }
        },
        [post]
    )

    const navigate = useNavigate();

    const getTags = () => {
        getAllTags().then((data) => setTags(data));
    };

    const toggle = () => setModal(!modal);
    const toggleDelete = () => setModalDelete(!modalDelete);
    const toggleEdit = () => setModalEdit(!modalEdit);

    const submit = () => {
        addPostTag(newPostTag);
        toggle();
        getPostTagsByPostId(id).then(setPostTags);
    };

    const deletePT = (evt) => {
        if (userProfile.id === post.userProfile.id) {
            deletePostTag(evt.target.id);
            getPostTagsByPostId(id).then(setPostTags);
        }
    };

    const deleteP = () => {
        if (userProfile.id === post.userProfile.id) {
            deletePost(id);
            navigate(`/myPosts`);
        }
    };

    const editP = () => {
        if (userProfile.id === post.userProfile.id) {
            editPost(id, newPost);
            navigate(`/myPosts`);
        }
    };

    useEffect(() => {
        getPostById(id).then(setPost);
        getPostTagsByPostId(id).then(setPostTags);
        getTags();
        const copy = { ...newPostTag };
        copy.postId = parseInt(id);
        setNewPostTag(copy);
    }, []);

    if (!post) {
        return null;
    }
    const date = new Date(post.createDateTime);

    return (
        <div className="container">
            <div className="HeaderImageContainer">
                {post.imageLocation === null ? (
                    <section>
                        <img
                            height="200px"
                            src="https://www.disneyclips.com/images/images/goofy_thinking.gif"
                        ></img>
                    </section>
                ) : (
                    <section>
                        <img height="200px" src={post.imageLocation}></img>
                    </section>
                )}
            </div>
            <div className="titles">
                <h1 className="title">{post.title}</h1>
                <h2 className="subtitle">{post.category.name}</h2>
            </div>

            <div className="tags">
                {postTags.map((pt) => {
                    return (
                        <Badge
                            color="primary"
                            className="badge"
                            id={pt?.id}
                            onClick={(evt) => deletePT(evt)}
                        >
                            {pt?.tag?.name}
                        </Badge>
                    );
                })}
            </div>
            <div className="AddButton">
                {userProfile?.id === post.userProfile.id ? (
                    <>
                        <Button onClick={toggle} className="addTagButton">
                            Add Tags to Post
                        </Button>
                        <Button color="secondary" onClick={toggleEdit}>
                            Edit Post
                        </Button>
                        <Button color="danger" onClick={toggleDelete}>
                            Delete Post
                        </Button>
                    </>
                ) : null}
            </div>

            <PostDetailsDeleteModal
                isOpen={modalDelete}
                toggle={toggleDelete}
                deleteP={deleteP}
            />
            <PostDetailsPostTagModal
                isOpen={modal}
                toggle={toggle}
                tags={tags}
                submit={submit}
                setNewPostTag={setNewPostTag}
                newPostTag={newPostTag}
            />
            {post ?
                <PostDetailsEditModal
                    isOpen={modalEdit}
                    toggle={toggleEdit}
                    editP={editP}
                    post={newPost}
                    editPost={updatePost}
                />
                : ""}
            <div className="PostDetailsBar">
                <div className="UserInfo">
                    {post.userProfile.imageLocation === null ? (
                        <section>
                            <img
                                height="30px"
                                src="https://www.disneyclips.com/images/images/goofy_thinking.gif"
                            ></img>
                        </section>
                    ) : (
                        <section>
                            <img height="30px" src={post?.userProfile.imageLocation}></img>
                        </section>
                    )}
                    <p>{post.userProfile.displayName}</p>
                </div>
                <div>
                    {date.getMonth() + 1}/{date.getDate()}/{date.getFullYear()}
                </div>
            </div>
            <div className="postBody">{post.content}</div>
        </div>
    );
};

export default PostDetails;
