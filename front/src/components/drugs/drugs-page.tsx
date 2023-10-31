import DrugCard from './drug-card';
import drugDatabase from './drug-database';




export function DrugsCardsContainer() {
  return (
    <div className="m-9 p-3 grid grid-cols-3 gap2 place-items-stretch rounded-lg bg-green-100 gap-3 ">
      {drugDatabase.map((drug) => (
        <DrugCard
          id={drug.id}
          key={drug.id}
          title={drug.title}
          description={drug.description}
          composition={drug.composition}
          price={drug.price}
          imgUrl={drug.imgUrl}
        />
      ))}
    </div>
  );
};


export default function DrugsPage() {
  return (
    <div className="container mx-auto">
      <DrugsCardsContainer />
    </div>
  )
}
