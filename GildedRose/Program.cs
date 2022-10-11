using System.Diagnostics.CodeAnalysis;

namespace GildedRose
{
    public class Program
    {
        public IList<Item> Items;

        [ExcludeFromCodeCoverage]
        public static void Main(string[] args)
        {
            var app = new Program()
            {
                Items = new List<Item>
                {
                    new Item { Name = "+5 Dexterity Vest", SellIn = 10, Quality = 20 },
                    new Item { Name = "Aged Brie", SellIn = 2, Quality = 0 },
                    new Item { Name = "Elixir of the Mongoose", SellIn = 5, Quality = 7 },
                    new Item { Name = "Sulfuras, Hand of Ragnaros", SellIn = 0, Quality = 80 },
                    new Item { Name = "Sulfuras, Hand of Ragnaros", SellIn = -1, Quality = 80 },
                    new Item { Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 15, Quality = 20 },
                    new Item { Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 10, Quality = 49 },
                    new Item { Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 5, Quality = 49 },
                    new ConjuredItem { Name = "Conjured Mana Cake", SellIn = 3, Quality = 6 }
                }
            };

            for (var i = 0; i < 31; i++)
            {
                Console.WriteLine("-------- day " + i + " --------");
                Console.WriteLine("name, sellIn, quality");
                for (var j = 0; j < app.Items.Count; j++)
                {
                    Console.WriteLine(app.Items[j].Name + ", " + app.Items[j].SellIn + ", " + app.Items[j].Quality);
                }
                Console.WriteLine("");
                app.UpdateQuality();
            }
        }

        public void UpdateQuality()
        {
            for (var i = 0; i < Items.Count; i++)
            {
                Items[i] = updateSellin(Items[i]);

                switch (Items[i], Items[i].Name)
                {
                    case (Item item, "Sulfuras, Hand of Ragnaros"):
                        break;
                    case (Item item, "Aged Brie"):
                        if (qualityIsUnder50(item))
                        {
                            item.Quality++;
                        }
                        break;
                    case (Item item, "Backstage passes to a TAFKAL80ETC concert"):
                        if (sellInIsPassed(item))
                        {
                            item.Quality = 0;
                        }
                        else if (qualityIsUnder50(item))
                        {
                            item.Quality++;

                            if (item.SellIn < 11 && qualityIsUnder50(item)) item.Quality++;
                            if (item.SellIn < 6 && qualityIsUnder50(item)) item.Quality++;
                        }
                        break;
                    case (ConjuredItem item, object):
                        if (sellInIsPassed(item))
                        {
                            item.Quality -= 4;
                        }
                        else if (qualityIsUnder50(item))
                        {
                            item.Quality -= 2;
                        }
                        break;
                    default:
                        //Update quality for standard item.
                        if (sellInIsPassed(Items[i]) && Items[i].Quality > 1)
                        {
                            Items[i].Quality -= 2;
                        }
                        else if (Items[i].Quality > 0)
                        {
                            Items[i].Quality -= 1;
                        }
                        break;
                }
            }
        }

        public Item updateSellin(Item item)
        {
            if (item.Name != "Sulfuras, Hand of Ragnaros")
            {
                item.SellIn--;
            }

            return item;
        }

        public bool sellInIsPassed(Item item) => item.SellIn < 0;

        public bool qualityIsUnder50(Item item) => item.Quality < 50;

    }

    public class Item
    {
        public string Name { get; set; }

        public int SellIn { get; set; }

        public int Quality { get; set; }
    }

    public class ConjuredItem : Item
    {

    }

}

