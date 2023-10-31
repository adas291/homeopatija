export interface DrugCardProps {
  id: string;
  title: string;
  description: string;
  composition: string[];
  price: number;
  imgUrl:string
}

const drugDatabase: DrugCardProps[] = [
  { id: "1", imgUrl: "/drug1.png", description: "padeda nuo uodų", title: "Ibuprofin", composition: ['Kava', 'Kiaušinis', 'Pipirai', 'Beržo drožlės'], price: 10.99 },
  { id: "2", imgUrl: "/drug2.png", description: "labai skanus", title: "Marsiečiai", composition: ['Kava', 'Kiaušinis', 'Pipirai', 'Soda'], price: 11.99 },
  { id: "3", imgUrl: "/drug3.png", description: "Padeda geriau uzmiršti sunkumus", title: "Bilobil", composition: ['Vanduo', 'Kiaušinis', 'Pipirai', 'Soda'], price: 6.99 },
  { id: "4", imgUrl: "/drug4.png", description: "Muša temperatura", title: "Procentamolis", composition: ['Kava', 'Mėtos', 'Pipirai', 'Soda'], price: 2.99 },
  { id: "5", imgUrl: "/drug5.png", description: "Malšina galvos skausmą", title: "Solpadojin", composition: ['Kava', 'Druska', 'Pipirai', 'Soda'], price: 8.99 },
  { id: "6", imgUrl: "/drug6.png", description: "Ataplaiduoja po darbų", title: "ksanoksas", composition: ['rasalas', 'Druska', 'Pipirai', 'milka'], price: 8.99 },

];


export default drugDatabase;
