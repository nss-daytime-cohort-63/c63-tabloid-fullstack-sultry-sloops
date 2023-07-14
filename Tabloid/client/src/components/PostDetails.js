import { useEffect, useState } from "react"
import { useParams } from "react-router-dom";
import { Post } from "./Post";
import { ListGroupItem } from "reactstrap";
import { getPostById } from "../modules/postManager";


const PostDetails = () => {
    const [post, setPost] = useState();
    const { id } = useParams();

    useEffect(() => {
        getPostById(id).then(setPost);
    }, []);

    if (!post) {
        return null;
    }
    const date = new Date(post.createDateTime)

    return (
        <div className="container">
            <div>
                {
                    post.imageLocation === null ? <section><img height="200px" src="https://www.disneyclips.com/images/images/goofy_thinking.gif"></img></section> : <section><img height="200px" src={post.imageLocation}></img></section>
                }
            </div>
            <h1>{post.title}</h1>
            <h2>{post.categoryId}</h2>
            <div>
                <div>
                    {
                        post.userProfile.imageLocation === null ? <section><img height="20px" src="https://www.disneyclips.com/images/images/goofy_thinking.gif"></img></section> : <section><img height="20px" src={post?.userProfile.imageLocation}></img></section>
                    }
                    <p>{post.userProfile.displayName}</p>
                </div>
                <div>Creation Date: {date.getMonth() + 1}/{date.getDate()}/{date.getFullYear()}</div>
            </div>
            <div>{post.content}</div>


        </div>
    );
};

export default PostDetails;