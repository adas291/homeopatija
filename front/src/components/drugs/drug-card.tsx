import { FunctionComponent } from 'react';
import { Link } from 'react-router-dom';
import { DrugCardProps } from './drug-database';


const DrugCard: FunctionComponent<DrugCardProps> = ({id, title, description, price, imgUrl}) => {
  return (
    <div className="flex flex-col justify-around   hover:bg-green-200   shadow-md shadow-gray-300 rounded-md bg-inherit border border-gray-500 p-2">
        <div className="flex items-center justify-center mb-2">
          <img width="150px" height="500px" src={imgUrl} alt="" />
        </div>
        <div className='text-lg font-semibold mb-2'>{title}</div>
        <div className='text-sm mb-2'>{description}</div>
        <div className='text-sm mb-2'>Kaina: ${price.toFixed(2)}</div>
        <div className="text-center">
          <button className="bg-transparent hover:bg-green-500 text-green-700 font-semibold hover:text-white py-2 px-4 border border-green-500 hover:border-transparent rounded">
            <Link to={`/vaistai/${id}`}>Peržiūrėti</Link>
          </button>
        </div>
    </div>
  );
};

export default DrugCard;
