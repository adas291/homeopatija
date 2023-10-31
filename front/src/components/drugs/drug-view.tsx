import CommentContainer from '../review/comment-container';
import { useParams } from 'react-router-dom';
import drugDatabase from './drug-database';


const comments = [
  { username: 'Ilona', body: 'Puiki sudėtis, greitai ir veiksmingai numalšino skausmą 🥰' },
  { username: 'Žaneta', body: 'Man nuo šito galva pradėjo skaudėti, nerekomenduoju' },
];



export default function DrugView(){

  //imam duomenis is db
  const { id } = useParams(); 
  const selectedDrug = drugDatabase.find((drug) => drug.id === id)

  if(selectedDrug == undefined) {
    return (
      <div>Vaistas nerastas duomenu bazeje</div>
    )

  }
  //komentarai visur vienodi by 

  return (
    <div className='container mx-auto'>
      <h1 className='text-3xl font-semibold text-green-700'>{selectedDrug.title}</h1>
      <div className='flex flex-row flex-wrap justify-around'>
        <div>
          <img
            className="overflow-ellipsis min-w-32 max-w-xs"
            src={selectedDrug.imgUrl}
            alt=""
          />
        </div>
        <div className='h-fit border border-gray-300 p-5'>
          <h2 className=' text-xl font-bold'>Sudėtis</h2>
          <ul className='list-disc list-inside'>
            {selectedDrug.composition.map((ingredient, index) => (
              <li key={index}>{ingredient}</li>
            ))}
          </ul>
          <br />
          <div>Kaina: {selectedDrug.price} EUR</div>

        </div>
      </div>
      <CommentContainer comments={comments}/>
    </div>
  );
};

