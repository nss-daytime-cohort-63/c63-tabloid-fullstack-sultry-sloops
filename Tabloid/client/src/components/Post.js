export const Post = ({ post }) => {
  return (
    <tr>
      <td>{post.title}</td>
      <td>{post?.userProfile?.displayName}</td>
      <td>{post.categoryId}</td>
    </tr>
  );
};
