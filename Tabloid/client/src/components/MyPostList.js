import { Post } from "./Post";
import { getPostsByUserId } from "../modules/postManager";
import { useState, useEffect } from "react";
import { Table } from "reactstrap";
import { getAllCategories } from "../modules/categoryManager";

export const MyPostList = ({ userProfile }) => {
    const [posts, setPosts] = useState([]);


    useEffect(() => {
        getPostsByUserId(userProfile.id).then(setPosts);
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
                {posts?.map((p) => {
                    return <Post post={p} key={p.id} />;
                })}
            </tbody>
        </Table>
    );
};
