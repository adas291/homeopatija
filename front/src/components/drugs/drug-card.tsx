import React, { FunctionComponent } from 'react';
import { Link } from 'react-router-dom';

interface DrugCardProps {
  id: number;
  name: string;
  description: string;
  price: number;
}

const DrugCard: FunctionComponent<DrugCardProps> = ({ id, name, description, price }) => {
  return (
    <Link to={`/vaistai/${id}`}>
      <div className="drug-card">
        <div className='drug-title'>{name}</div>
        <div className='drug-body'>{description}</div>
        <b>Kaina: ${price.toFixed(2)}</b>
      </div>
    </Link>
  );
};

export default DrugCard;
