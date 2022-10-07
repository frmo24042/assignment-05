namespace GildedRose.Tests;

public class ProgramTests
{
    Program program = new Program();
    public ProgramTests()
    {
        program.Items = new List<Item> { };

    }

    [Fact]
    public void Create_New_Object_To_List_Checks_If_Quality_Has_Decreased_After_Update()
    {
        // Given
        var items = program.Items;
        var itemToCheck = new Item { Name = "foo", SellIn = 0, Quality = 20 };
        items.Add(itemToCheck);

        // When
        program.UpdateQuality();

        // Then
        Assert.True(itemToCheck.Quality < 20);
    }

    [Fact]
    public void Create_Object_To_List_Checks_If_Sellin_Has_Decreased_After_Update()
    {
        // Given
        var items = program.Items;
        var itemToCheck = new Item { Name = "foo", SellIn = 20, Quality = 0 };
        items.Add(itemToCheck);

        // When
        program.UpdateQuality();

        // Then
        Assert.True(itemToCheck.SellIn < 20);
    }

    [Fact]
    public void Given_Item_Checks_If_Quality_Degrades_Twice_As_Fast_After_Sellin()
    {
        // Given
        var items = program.Items;
        var itemToCheck = new Item { Name = "foo", SellIn = 1, Quality = 20 };
        items.Add(itemToCheck);

        // When
        program.UpdateQuality();
        var firstQualityCheck = items.Last().Quality;
        program.UpdateQuality();
        var secondQualityCheck = items.Last().Quality;

        // Then
        Assert.False(20 - firstQualityCheck == firstQualityCheck - secondQualityCheck);
        Assert.True(firstQualityCheck - secondQualityCheck == (20 - firstQualityCheck) * 2);
    }

    [Fact]
    public void Given_Item_With_0_Quality_Checks_If_Quality_Does_Goes_Negative()
    {
        // Given
        var items = program.Items;
        var itemToCheck = new Item { Name = "foo", SellIn = 1, Quality = 0 };
        items.Add(itemToCheck);

        // When
        program.UpdateQuality();

        // Then
        Assert.True(items.Last().Quality >= 0);
    }

    [Fact]
    public void AgedBrie_Increases_In_Quality_When_It_Gets_Older()
    {
        // Given
        var Items = program.Items;
        var AgedBrie = new Item { Name = "Aged Brie", SellIn = 10, Quality = 10 };
        Items.Add(AgedBrie);

        // When
        program.UpdateQuality();

        // Then
        AgedBrie.Quality.Should().Be(11);
    }

    [Fact]
    public void Given_AgedBrie_With_50_Quality_When_Update_Then_Quality_Does_Not_Increase()
    {
        // Given
        var items = program.Items;
        var itemToCheck = new Item { Name = "Aged Brie", SellIn = 1, Quality = 50 };
        items.Add(itemToCheck);

        // When
        program.UpdateQuality();

        // Then
        Assert.True(items.Last().Quality == 50);
    }

    [Fact]
    public void Given_BackstagePasses_When_Update_Increases_In_Quality()
    {
        // Given
        var items = program.Items;
        var itemToCheck = new Item { Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 20, Quality = 20 };
        items.Add(itemToCheck);

        // When
        program.UpdateQuality();

        // Then
        Assert.True(items.Last().Quality > 20);
    }

    [Fact]
    public void Given_BackstagePasses_With_Sellin_Under10_When_Update_Increases_by_Two_In_Quality()
    {
        // Given
        var items = program.Items;
        var itemToCheck = new Item { Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 9, Quality = 20 };
        items.Add(itemToCheck);

        // When
        program.UpdateQuality();

        // Then
        Assert.True(items.Last().Quality == 22);
    }

    [Fact]
    public void Given_BackstagePasses_With_Sellin_Under5_When_Update_Increases_by_Three_In_Quality()
    {
        // Given
        var items = program.Items;
        var itemToCheck = new Item { Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 4, Quality = 20 };
        items.Add(itemToCheck);

        // When
        program.UpdateQuality();

        // Then
        Assert.True(items.Last().Quality == 23);
    }

    [Fact]
    public void Given_BackstagePasses_With_Negative_Sellin_When_Update_Then_Quality_Drops_To_0()
    {
        // Given
        var items = program.Items;
        var itemToCheck = new Item { Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 0, Quality = 20 };
        items.Add(itemToCheck);

        // When
        program.UpdateQuality();

        // Then
        Assert.True(items.Last().Quality == 0);
    }

    [Fact]
    public void Sulfuras_Sellin_Should_Not_Change_After_UpdateQuality()
    {
        // Given
        var Items = program.Items;
        var Sulfuras = new Item { Name = "Sulfuras, Hand of Ragnaros", SellIn = 0, Quality = 80 };
        Items.Add(Sulfuras);

        // When
        program.UpdateQuality();

        // Then
        Sulfuras.SellIn.Should().Be(0);
    }

    [Fact]
    public void Sulfuras_Quality_Should_Not_Change_After_UpdateQuality()
    {
        // Given
        var Items = program.Items;
        var Sulfuras = new Item { Name = "Sulfuras, Hand of Ragnaros", SellIn = 0, Quality = 80 };
        Items.Add(Sulfuras);

        // When
        program.UpdateQuality();

        // Then
        Sulfuras.Quality.Should().Be(80);
    }


}