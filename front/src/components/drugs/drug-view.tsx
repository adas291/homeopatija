import React from 'react';
import CommentContainer from '../review/comment-container';

interface DrugCardProps {
  title: string;
  composition: string[];
  price: number;
}

const comments = [
  { username: 'Ilona', body: 'Puiki sudėtis, greitai ir veiksmingai numalšino skausmą 🥰' },
  { username: 'Žaneta', body: 'Man nuo šito galva pradėjo skaudėti, nerekomenduoju' },
  // Add more comments as needed
];



const DrugView: React.FC<DrugCardProps> = ({title, composition, price}) => {
  return (
    <div className='container'>
      <h1>{title}</h1>
      <h2>Sudėtis</h2>
      <ul>
        {composition.map((ingredient, index) => (
          <li key={index}>{ingredient}</li>
        ))}
      </ul>
      <p>Kaina: {price} EUR</p>

      <CommentContainer comments={comments}/>
    </div>
  );
};

export default DrugView;
