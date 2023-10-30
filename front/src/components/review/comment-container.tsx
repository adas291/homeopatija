import React from 'react';

// Comment component
interface CommentProps {
  username: string;
  body: string;
}


function Comment({ username, body }: CommentProps) {
  return (
    <div className="comment-container">
      <div className='comment-header'>
        <div className="user-name">{username}</div>
        <button>Pranešti</button>
      </div>
      <div className="comment-body">{body}</div>
    </div>
  );
}

// CommentContainer component that holds multiple comments
interface CommentContainerProps {
  comments: CommentProps[];
}

function CommentContainer({ comments }: CommentContainerProps) {
  return (
    <div className="comment-section">
      {comments.map((comment, index) => (
        <Comment key={index} username={comment.username} body={comment.body} />
      ))}
    </div>
  );
}

export default CommentContainer;
