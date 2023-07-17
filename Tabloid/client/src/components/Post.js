import { Link } from "react-router-dom";

export const Post = ({ post }) => {
    return (
        <tr>

            <td><Link to={`/posts/${post.id}`}>{post.title}</Link></td>
            <td>{post?.userProfile?.displayName}</td>
            <td>{post.category.name}</td>

        </tr >
    );
};
