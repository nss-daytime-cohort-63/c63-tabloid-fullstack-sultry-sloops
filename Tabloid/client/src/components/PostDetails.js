import { useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import { Post } from "./Post";
import { getPostById } from "../modules/postManager";
import {
  Button,
  Modal,
  ModalHeader,
  ModalBody,
  ModalFooter,
  FormGroup,
  Input,
  Label,
} from "reactstrap";
import { getAllTags } from "../modules/tagManager";
import { addPostTag } from "../modules/postTagManager";

const PostDetails = ({ userProfile }) => {
  const [post, setPost] = useState();
  const { id } = useParams();
  const [modal, setModal] = useState(false);
  const [tags, setTags] = useState([]);
  const [postTag, setPostTag] = useState({
    postId: 0,
    tagId: 0,
  });

  const getTags = () => {
    getAllTags().then((data) => setTags(data));
  };

  const toggle = () => setModal(!modal);

  const submit = () => {
    addPostTag(postTag);
    toggle();
  };

  useEffect(() => {
    getPostById(id).then(setPost);
    getTags();
    const copy = { ...postTag };
    copy.postId = parseInt(id);
    setPostTag(copy);
  }, []);

  if (!post) {
    return null;
  }
  const date = new Date(post.createDateTime);

  return (
    <div className="container">
      <div>
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
      <h1>{post.title}</h1>
      <h2>{post.category.name}</h2>
      {userProfile.id === post.userProfile.id ? (
        <Button onClick={toggle}>Add Tags to Post</Button>
      ) : null}
      {toggle ? (
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
                              const copy = { ...postTag };
                              copy.tagId = t.id;
                              setPostTag(copy);
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
      <div>
        <div>
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
          Creation Date: {date.getMonth() + 1}/{date.getDate()}/
          {date.getFullYear()}
        </div>
      </div>
      <div>{post.content}</div>
    </div>
  );
};

export default PostDetails;
