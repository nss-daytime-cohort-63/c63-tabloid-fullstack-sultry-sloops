import { useEffect, useState } from "react";
import { useNavigate, useParams } from "react-router-dom";
import { Post } from "./Post";
import { deletePost, getAllPosts, getPostById } from "../modules/postManager";
import {
    Button,
    Modal,
    ModalHeader,
    ModalBody,
    ModalFooter,
    FormGroup,
    Input,
    Label,
    Badge,
} from "reactstrap";
import { getAllTags } from "../modules/tagManager";
import {
    addPostTag,
    getPostTagsByPostId,
    deletePostTag,
} from "../modules/postTagManager";
import "./postDetails.css";

const PostDetails = ({ userProfile }) => {
    const [post, setPost] = useState();
    const { id } = useParams();
    const [modal, setModal] = useState(false);
    const [modalDelete, setModalDelete] = useState(false);
    const [tags, setTags] = useState([]);
    const [postTags, setPostTags] = useState([]);
    const [deleteTag, setDeleteTag] = useState();
    const [newPostTag, setNewPostTag] = useState({
        postId: 0,
        tagId: 0,
    });

    const navigate = useNavigate();

    const getTags = () => {
        getAllTags().then((data) => setTags(data));
    };

    const toggle = () => setModal(!modal);
    const toggleDelete = () => setModalDelete(!modalDelete);


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

    const deleteP = (evt) => {
        if (userProfile.id === post.userProfile.id) {
            deletePost(id);
            navigate(`/myPosts`)
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
                    <><Button onClick={toggle} className="addTagButton">
                        Add Tags to Post
                    </Button>
                        <Button color="danger">Delete Post</Button></>
                ) : null}
            </div>

            {modalDelete ? (
                <div>
                    <Modal isOpen={modalDelete} toggle={toggleDelete}>
                        <ModalHeader toggle={toggleDelete}>Delete Confirmation</ModalHeader>
                        <ModalBody>
                            Are you sure you want to delete this post?
                        </ModalBody>
                        <ModalFooter>
                            <Button color="danger" onClick={deleteP}>
                                Confirm Delete
                            </Button>{" "}
                            <Button color="secondary" onClick={toggleDelete}>
                                Cancel
                            </Button>
                        </ModalFooter>
                    </Modal>
                </div>
            ) : null}

            {modal ? (
                <div>
                    <Modal isOpen={modal} toggle={toggle}>
                        <ModalHeader toggle={toggle}>Add a Tag to Post</ModalHeader>
                        <ModalBody>
                            <FormGroup>
                                {tags.map((t) => {
                                    return (
                                        <>
                                            <div>
                                                <FormGroup>
                                                    <Input
                                                        bsSize=""
                                                        type="radio"
                                                        name="tag"
                                                        onClick={() => {
                                                            const copy = { ...newPostTag };
                                                            copy.tagId = t.id;
                                                            setNewPostTag(copy);
                                                        }}
                                                    />
                                                    <Label>
                                                        {t.name} - {t.id}
                                                    </Label>
                                                </FormGroup>
                                            </div>
                                        </>
                                    );
                                })}
                            </FormGroup>
                        </ModalBody>
                        <ModalFooter>
                            <Button color="primary" onClick={submit}>
                                Submit Post Tag
                            </Button>{" "}
                            <Button color="secondary" onClick={toggle}>
                                Cancel
                            </Button>
                        </ModalFooter>
                    </Modal>
                </div>
            ) : null}
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
