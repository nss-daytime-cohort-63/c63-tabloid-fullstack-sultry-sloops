import { Post } from "./Post";
import { getPostByUserId } from "../modules/postManager";
import { useState, useEffect } from "react";
import { Table } from "reactstrap";

export const PostList = () => {
    const [posts, setPosts] = useState([]);

    const getPosts = () => {
        getPostByUserId().then((data) => setPosts(data));
    };

    useEffect(() => {
        getPosts();
    }, []);

    return (
        <Table hover responsive size="">
            <thead>
                <tr>
                    <th>Title</th>
                    <th>Author</th>
                    <th>Category</th>
                </tr>
            </thead>
            <tbody>
                {posts.map((p) => {
                    return <Post post={p} key={p.id} />;
                })}
            </tbody>
        </Table>
    );
};
