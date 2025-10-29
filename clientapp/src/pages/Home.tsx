import Slider from '../components/Slider';

export default function Home() {
  return (
    <div className="p-8 text-center">
      <h1 className="text-4xl font-bold mb-4">Табачные изделия для ценителей</h1>
      <p className="text-lg mb-8">
        Качество, стиль и уверенность. Откройте для себя наш каталог.
      </p>
        <Slider />
    </div>
  );
}