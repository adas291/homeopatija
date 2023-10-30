import React, { FunctionComponent } from 'react';
import DrugCard from './drug-card';

interface Drug {
  id: number;
  name: string;
  description: string;
  price: number;
}

interface DrugContainerProps {
  // No need for 'drugs' prop since data is preinitialized
}

const preinitializedDrugs: Drug[] = [
  {
    id: 1,
    name: 'Aspirin',
    description: 'Pain reliever',
    price: 5.99,
  },
  {
    id: 2,
    name: 'Ibuprofen',
    description: 'Anti-inflammatory',
    price: 7.49,
  },
  // Add more preinitialized drugs as needed
];

const DrugContainer: FunctionComponent<DrugContainerProps> = () => {
  return (
    <div className="drug-container">
      {preinitializedDrugs.map((drug) => (
        <DrugCard
          id={drug.id}
          key={drug.id}
          name={drug.name}
          description={drug.description}
          price={drug.price}
        />
      ))}
    </div>
  );
};

export default DrugContainer;
